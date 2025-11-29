using LibrarySystem.BusnissLogic.Dtos.BookDtos;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.BusnissLogic.ServicesInterfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(int id , [FromForm] Book book);
        Task<Book> DeleteBookAsync( int id );
        Task<List<Book>> SearchBooksAsync(string keyword);
        Task<bool> IsBookAvailableAsync(int id);
    }
}
