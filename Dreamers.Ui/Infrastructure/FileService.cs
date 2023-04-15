namespace Dreamers.Ui.Infrastructure
{
    public class FileService : IFileService
    {
        async Task<bool> IFileService.UploadFile(IFormFile file, string folderName)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @$"wwwroot\photos\{folderName}", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return true;
            }
            return false;
        }
    }
}
