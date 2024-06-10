using Application.Commons;
using Microsoft.AspNetCore.StaticFiles;
using static Azure.Core.HttpHeader;

namespace UniProjectHub_BE.Services
{
    public class ManageImage : IManageImage
    {
        public async Task<string> UploadFile(IFormFile _IFormFile)
        {
            string FileName = "";
            try
            {
                FileInfo _FileInfo = new FileInfo(_IFormFile.FileName);
                FileName = _IFormFile.FileName + "_" + DateTime.Now.Ticks.ToString() + _FileInfo.Extension;
                var _GetFilePath = FileHelper.GetFilePath(FileName);
                using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
                {
                    await _IFormFile.CopyToAsync(_FileStream);
                }

                return FileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<(byte[], string, string)> DownloadFile(string FileName)
        {
            try
            {
                var _GetFilePath = FileHelper.GetFilePath(FileName);
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(_GetFilePath, out var _ContentType))
                {
                    _ContentType = "application/octet-stream";
                }
                var _ReadAllBytesAsync = await File.ReadAllBytesAsync(_GetFilePath);
                return (_ReadAllBytesAsync, _ContentType, Path.GetFileName(_GetFilePath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RemoveFile(string FileName)
        {
            try
            {
                var _GetFilePath = FileHelper.GetFilePath(FileName);
                if (File.Exists(_GetFilePath))
                {
                    File.Delete(_GetFilePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
