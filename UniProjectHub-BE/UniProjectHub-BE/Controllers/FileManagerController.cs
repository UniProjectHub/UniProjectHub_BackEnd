using Application.InterfaceServies;
using Application.Services;
using Application.ViewModels.FileViewModel;
using Application.ViewModels.GroupChatViewModel;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UniProjectHub_BE.Services;

namespace UniProjectHub_BE.Controllers
{
    public class FileManagerController : Controller
    {
        private readonly IManageImage _iManageImage;
        private readonly IFileManageService _fileService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<FileManagerController> _logger;

        public FileManagerController(IManageImage iManageImage, IFileManageService fileService, ITokenService tokenService, ILogger<FileManagerController> logger)
        {
            _iManageImage = iManageImage;
            _fileService = fileService;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost]
        [Route("uploadfile")]
        public async Task<IActionResult> UploadFile(IFormFile _IFormFile, FileViewModel fileViewModel)
        {
            var uploadResult = await _iManageImage.UploadFile(_IFormFile);
            if (uploadResult == null)
            {
                return BadRequest("Upload fail");
            }
            FileViewModel file = new FileViewModel { 
                CreatedAt = DateTime.Now,
                Filename = uploadResult,
                TaskId = fileViewModel.TaskId,
                UserId = fileViewModel.UserId,
            };
            var result = await _fileService.CreateFileAsync(file);

            return Ok(result);
        }

        //[HttpGet]
        //[Route("downloadfile")]
        //public async Task<IActionResult> DownloadFile(string FileName)
        //{
        //    var result = await _iManageImage.DownloadFile(FileName);
        //    return File(result.Item1, result.Item2, result.Item2);
        //}

        [HttpGet("downloadfile")]
        public async Task<IActionResult> DownloadFile(string token)
        {
            _logger.LogInformation($"Attempting to download file with token: {token}");

            token = WebUtility.UrlDecode(token); // Decode the token

            if (!_tokenService.ValidateToken(token, out var fileName))
            {
                _logger.LogWarning($"Invalid or expired token: {token}");
                return Unauthorized();
            }

            var result = await _iManageImage.DownloadFile(fileName);
            _logger.LogInformation($"Successfully downloaded file: {fileName}");
            return File(result.Item1, result.Item2, result.Item3); // Use result.Item3 for the file name
        }

        [HttpGet("generatedownloadlink")]
        public IActionResult GenerateDownloadLink(string fileName)
        {
            var token = _tokenService.GenerateDownloadFileToken(fileName);
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Unable to generate download link.");
            }

            var downloadLink = Url.Action("DownloadFile", "FileManager", new { token }, Request.Scheme);
            if (string.IsNullOrEmpty(downloadLink))
            {
                return BadRequest("Unable to generate download link.");
            }

            return Ok(new { DownloadLink = downloadLink });
        }

        [HttpDelete]
        [Route("removefile")]
        public IActionResult RemoveFile(string FileName)
        {
            var result = _iManageImage.RemoveFile(FileName);
            if (result)
            {
                return Ok(new { message = "File deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "File not found." });
            }
        }
    }
}
