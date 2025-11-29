namespace LibrarySystem.BusnissLogic.ServicesInterfaces
{
    public interface IFileUploadsService
    {
        
            Task<string> UploadImageAsync(IFormFile file);
            Task<bool> DeleteImageAsync(string fileName);
            Task<string> UpdateImageAsync(IFormFile newFile, string oldFileName);

        }

    }

