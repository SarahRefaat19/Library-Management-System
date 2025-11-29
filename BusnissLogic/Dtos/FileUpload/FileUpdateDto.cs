namespace LibrarySystem.BusnissLogic.Dtos.FileUpload
{
    public class FileUpdateDto
    {
        public IFormFile File { get; set; } = null!;
        public string OldFileName { get; set; } = null!;
    }
}
