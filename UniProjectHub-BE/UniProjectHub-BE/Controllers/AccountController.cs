using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Account;
using Domain.Interfaces;
using Domain.Models;
using Infracstructures.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using UniProjectHub_BE.Services;
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
        private readonly ManageFisebase manageFirebase;
        private readonly ICurrentUserService currentUserService;

        public AccountController(
            UserManager<Users> userManager, 
            ITokenService tokenService, 
            SignInManager<Users> signInManager,
            Domain.Interfaces.IEmailSender emailSender,
            ManageFisebase manageFirebase,
            ICurrentUserService currentUserService
            )
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.manageFirebase = manageFirebase;
            this.currentUserService = currentUserService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.FindByNameAsync(loginDto.Username);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username!" });
            }

            if (!user.EmailConfirmed)
            {
                return Unauthorized(new { message = "Email not confirmed." });
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            var accessToken = await tokenService.GenerateToken(user);
            var refreshToken = tokenService.GenerateRefreshToken();
            tokenService.SaveRefreshToken(user.UserName, refreshToken);

            return Ok(new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Invalid user.");
            }

            tokenService.RemoveRefreshToken(userId);
            await signInManager.SignOutAsync();

            return Ok("Logout successful.");
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenModel tokenModel)
        {
            var refreshToken = tokenService.GetRefreshToken(tokenModel.RefreshToken);
            if (refreshToken == null)
            {
                return Unauthorized();
            }

            var user = userManager.Users.SingleOrDefault(u => u.Id == refreshToken.UserId);
            if (user == null)
            {
                return Unauthorized();
            }

            var newAccessToken = await tokenService.GenerateToken(user);
            var newRefreshToken = tokenService.GenerateRefreshToken();

            tokenService.RemoveRefreshToken(tokenModel.RefreshToken);
            tokenService.SaveRefreshToken(user.Id, newRefreshToken);

            return Ok(new TokenModel { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
        }

        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null || user.EmailConfirmed)
                {
                    // Don't reveal that the user does not exist or is already confirmed
                    return Ok(new { message = "Confirmation email sent. Please check your email." });
                }

                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token }, Request.Scheme);

                await emailSender.SendEmailAsync(model.Email, "Confirm your email", confirmationLink);

                return Ok(new { message = "Confirmation email sent. Please check your email." });
            }

            return BadRequest();
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
                                Token = tokenService.GenerateToken(user).Result
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
        [HttpGet("confirm-email/{userId}")]
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
                return BadRequest("User not found.");
            }
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                //user.RegistrationTime = DateTime.Now; // Update the LastLoginTime property
                //await userManager.UpdateAsync(user); // Save the changes to the user entity
                //return View("ConfirmEmail");
                return Ok(new { message = "Email confirmed successfully!" });
            }
            else
            {
                return BadRequest("Error confirming your email.");
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

        [HttpGet("login-google")]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = "api/account/callback-google";
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [HttpGet("callback-google")]
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

        [Authorize]
        [HttpPut()]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileDto updateUserProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await currentUserService.GetUser();
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var url = manageFirebase.ImageURL(updateUserProfileDto.Avatar);

            user.FirstName = updateUserProfileDto.FirstName;
            user.LastName = updateUserProfileDto.LastName;
            user.PhoneNumber = updateUserProfileDto.PhoneNumber;
            user.DateOfBirth = updateUserProfileDto.DateOfBirth;
            user.IsStudent = updateUserProfileDto.IsStudent;
            user.University = updateUserProfileDto.University;
            user.IsMale = updateUserProfileDto.IsMale;
            user.AvatarURL = url;

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