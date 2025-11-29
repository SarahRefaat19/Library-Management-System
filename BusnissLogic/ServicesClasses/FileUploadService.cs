
using LibrarySystem.BusnissLogic.ServicesInterfaces;
namespace LibrarySystem.BusnissLogic.ServicesClasses
{
    public class FileUploadService: IFileUploadsService
    {

        private readonly string _UploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        private const long _maxFileSize = 4 * 1024 * 1024; 

        public async Task<string> UploadImageAsync(IFormFile Imagefile)
        {
            if (Imagefile == null|| Imagefile.Length==0) 
                throw new ArgumentNullException("invalid image file ");

            if (!Directory.Exists(_UploadFolder))
                Directory.CreateDirectory(_UploadFolder);
            var filename = Guid.NewGuid().ToString()+ Path.GetExtension(Imagefile.FileName);
            var filePath = Path.Combine(_UploadFolder, filename);

            if (!_allowedExtensions.Contains(filename))
                throw new Exception("Only image files are allowed!");

            if (Imagefile.Length > _maxFileSize)
                throw new Exception("File size exceeds 4MB limit");

            using (var stream =new FileStream(filePath, FileMode.Create))
            {
                await Imagefile.CopyToAsync(stream);
            }
            return filename; 

        }





        public async Task<bool> DeleteImageAsync( string oldImage)
        {
            if (string.IsNullOrEmpty(oldImage))
                return false;

            var filePath = Path.Combine(_UploadFolder, oldImage);

            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
                return true;
            }

            return false;
        
        }





        public async Task<string> UpdateImageAsync(IFormFile Imagefile , string oldImage)
        {

            await DeleteImageAsync(oldImage);

            return await UploadImageAsync(Imagefile);
        }





    }
}
