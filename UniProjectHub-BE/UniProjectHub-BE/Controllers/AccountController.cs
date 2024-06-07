using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Account;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Users> userManager;
        private readonly ITokenService tokenService;
        private readonly SignInManager<Users> signInManager;
        private readonly Domain.Interfaces.IEmailSender emailSender;

        public AccountController(
            UserManager<Users> userManager, 
            ITokenService tokenService, 
            SignInManager<Users> signInManager,
            Domain.Interfaces.IEmailSender emailSender
            )
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = tokenService.CreateToken(user)
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new Users
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    EmailConfirmed = false
                };

                var createdUser = await userManager.CreateAsync(user, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await userManager.AddToRoleAsync(user, "User");
                    if (roleResult.Succeeded)
                    {
                        // Phát sinh token để xác nhận email
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                        // https://localhost:5001/confirm-email?userId=fdsfds&token=xyz&returnUrl=
                        var callbackUrl = Request.Scheme + "://" + Request.Host + Url.Action("confirmemail", "account", new { userId = user.Id, token = token });

                        await emailSender.SendEmailAsync(user.Email,
                            "Confirm email", callbackUrl);

                        if (userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            
                             
                        }
                        else
                        {
                            await signInManager.SignInAsync(user, isPersistent: false);
                            return Ok("Create account OK");
                        }

                        return Ok(
                            new NewUserDto
                            {
                                UserName = user.UserName,
                                Email = user.Email,
                                Token = tokenService.CreateToken(user)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        // GET: /Account/ConfirmEmail
        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest("Null userid, token");
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Null user");
            }
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                //user.RegistrationTime = DateTime.Now; // Update the LastLoginTime property
                //await _userManager.UpdateAsync(user); // Save the changes to the user entity
                //return View("ConfirmEmail");
                return Ok("Confirm OK");
            }
            else
            {
                return BadRequest("Confirm fail!!");
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
                return BadRequest("User not found.");

            // Phát sinh token để xác nhận email
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            // https://localhost:5001/confirm-email?userId=fdsfds&token=xyz&returnUrl=
            var callbackUrl =  Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

            await emailSender.SendEmailAsync(user.Email,
                "Forgot password", callbackUrl);
            Console.WriteLine("Token: " + token);
            return Ok("Password reset link has been sent to your email.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return BadRequest("User not found.");

            var resetPassResult = await userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);

            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return Ok("Password has been reset successfully.");
        }

        [HttpGet("login-Google")]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = "api/account/CallBackGoogle";
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [HttpGet("callback-Google")]
        public async Task<ActionResult> CallBackGoogle()
        {
            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return BadRequest("Null info");

            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            string[] userInfo = { info.Principal.FindFirst(ClaimTypes.Name).Value, info.Principal.FindFirst(ClaimTypes.Email).Value };
            if (result.Succeeded)
                return Ok(userInfo);
            else
            {
                Users user = new Users
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
                };

                IdentityResult identResult = await userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, false);
                        return Ok("Create new user with Google login");
                    }
                }
                return BadRequest();
            }
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileDto updateUserProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.FirstName = updateUserProfileDto.FirstName;
            user.LastName = updateUserProfileDto.LastName;
            user.PhoneNumber = updateUserProfileDto.PhoneNumber;
            user.DateOfBirth = updateUserProfileDto.DateOfBirth;
            user.IsStudent = updateUserProfileDto.IsStudent;
            user.University = updateUserProfileDto.University;
            user.IsMale = updateUserProfileDto.IsMale;
            user.AvatarURL = updateUserProfileDto.AvatarURL;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok(new { message = "Profile updated successfully." });
        }

        private void SendEmail(string body, string email)
        {
            //var emailBody = "Pleease confirm email <a href=\"#URL#\"> Click here </a>";
            //var callbackUrl = Request.Scheme + "://" + Request.Host + Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code });
            //var body = emailBody.Replace("#URL#", System.Text.Encodings.Web.HtmlEncoder.Default.Encode(callbackUrl));
            //Create client 
            //var client; ;
        }
    }
}