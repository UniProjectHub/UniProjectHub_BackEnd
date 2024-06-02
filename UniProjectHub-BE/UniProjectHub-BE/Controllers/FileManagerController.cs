using Microsoft.AspNetCore.Mvc;
using UniProjectHub_BE.Services;

namespace UniProjectHub_BE.Controllers
{
    public class FileManagerController : Controller
    {
        private readonly IManageImage _iManageImage;
        public FileManagerController(IManageImage iManageImage)
        {
            _iManageImage = iManageImage;
        }

        [HttpPost]
        [Route("uploadfile")]
        public async Task<IActionResult> UploadFile(IFormFile _IFormFile)
        {
            var result = await _iManageImage.UploadFile(_IFormFile);
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
