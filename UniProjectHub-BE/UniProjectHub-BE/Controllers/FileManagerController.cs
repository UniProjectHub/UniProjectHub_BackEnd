using Application.InterfaceServies;
using Application.Services;
using Application.ViewModels.FileViewModel;
using Application.ViewModels.GroupChatViewModel;
using Microsoft.AspNetCore.Mvc;
using UniProjectHub_BE.Services;

namespace UniProjectHub_BE.Controllers
{
    public class FileManagerController : Controller
    {
        private readonly IManageImage _iManageImage;
        private readonly IFileManageService _fileService;

        public FileManagerController(IManageImage iManageImage, IFileManageService fileService)
        {
            _iManageImage = iManageImage;
            _fileService = fileService;
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

        [HttpGet]
        [Route("downloadfile")]
        public async Task<IActionResult> DownloadFile(string FileName)
        {
            var result = await _iManageImage.DownloadFile(FileName);
            return File(result.Item1, result.Item2, result.Item2);
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
