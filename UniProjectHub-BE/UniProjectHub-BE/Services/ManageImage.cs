using Application.Commons;
using Application.ViewModels.FileViewModel;
using Microsoft.AspNetCore.StaticFiles;
using static Azure.Core.HttpHeader;

namespace UniProjectHub_BE.Services
{
    public class ManageImage : IManageImage
    {
        const long MaxFileSize = 6 * 1024 * 1024;

        public async Task<FileViewModel> UploadFile(IFormFile _IFormFile)
        {
            string FileName = "";
            string RealFileName = "";
            try
            {
                FileInfo _FileInfo = new FileInfo(_IFormFile.FileName);
                FileName = _IFormFile.FileName;
                RealFileName = _IFormFile.FileName + "_" + DateTime.Now.Ticks.ToString() + _FileInfo.Extension;
                var _GetFilePath = FileHelper.GetFilePath(RealFileName);
                using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
                {
                    await _IFormFile.CopyToAsync(_FileStream);
                }

                return new FileViewModel { Filename = FileName, RealFileName = RealFileName };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<(byte[], string, string)> DownloadFile(string fileName)
        {
            try
            {
                var _GetFilePath = FileHelper.GetFilePath(fileName);
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

        public (bool, string) ValidateFileSize(IFormFile file)
        {
            const long MaxFileSize = 6 * 1024 * 1024; // 6MB in bytes

            if (file.Length > MaxFileSize)
            {
                return (false, "File size exceeds the 6MB limit.");
            }

            return (true, string.Empty);
        }
    }
}
