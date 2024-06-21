using Application.ViewModels.FileViewModel;

namespace UniProjectHub_BE.Services
{
    public interface IManageImage
    {
        Task<FileViewModel> UploadFile(IFormFile _IFormFile);
        Task<(byte[], string, string)> DownloadFile(string fileName);
        bool RemoveFile(string FileName);
        public (bool, string) ValidateFileSize(IFormFile file);
    }
}
