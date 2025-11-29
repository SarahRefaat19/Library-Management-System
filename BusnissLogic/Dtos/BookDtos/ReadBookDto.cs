using LibrarySystem.Domain.Entities;

namespace LibrarySystem.BusnissLogic.Dtos.BookDtos
{
    public class ReadBookDto
    {
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string? CategoryName { get; set; } 
        public BookStatus Status { get; set; }
        public string ImageUrl { get; set; } = " ";


    }
}
