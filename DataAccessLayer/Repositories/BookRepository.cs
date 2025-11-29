using LibrarySystem.Domain.Entities;     // Book
using LibrarySystem.Domain.Interfaces;   // IBookRepository, IGenericRepository
using Microsoft.EntityFrameworkCore;     // EF Core (ToListAsync, AnyAsync, FindAsync, SaveChangesAsync)
using System.Collections.Generic;        // List<T>
using System.Threading.Tasks;            // Task<T>
using System.Linq;


namespace LibrarySystem.DataAccessLayer.Repositories
{
    public class BookRepository : IbookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)   //Constructor for make system Apply Dependency Injection 
        {
            _context = context;
        }



         public async Task<List<Book>> GetAllAsync() => await _context.Books.ToListAsync();


        public async Task<Book?> GetbyIdAsync(int id)
        {
            
            return await _context.Books.FindAsync(id);
        }


        public async Task<Book> AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }


        public async Task<Book> UpdateAsync(Book book)
        {
             _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }



        public async Task<Book> DeleteAsync(Book book)
        {
             _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<List<Book>> SearchBooksAsync(string keyword)
        {
            return await _context.Books.Where(b => b.Title.Contains(keyword) || b.Description.Contains(keyword)).ToListAsync(); //// Get books where the title or description contains the keyword
        }
        public async Task<bool> IsAvailableBookAsync(int id )
        {
            return  _context.Books.Any(b => b.Id ==id && b.IsAvailable); 

        }

       
    }
}
