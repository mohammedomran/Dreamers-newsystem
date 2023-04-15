namespace Dreamers.Ui.Infrastructure
{
    public interface IFileService
    {
        Task<bool> UploadFile(IFormFile file, string folderName);
    }
}
