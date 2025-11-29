using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace LibrarySystem.DataAccessLayer.Repositories
{
    public class AuthorRepository : IGenericRepository<Author>, IAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Author>> GetAllAuthorsWithBooksAsync()
        {
            return await _context.Authors
                .Include(a => a.Books)
                .ToListAsync();


        }
        public async Task<IEnumerable<Author>> GetAuthorWithBooksByIdAsync(int id)
        {
            return await _context.Authors
                .Include(a => a.Books)
                .ToListAsync();
        }
      public async  Task<List<Author>> SearchByNameAsync(string name)
        {
            return await _context.Authors.Where(A => A.Name.Contains(name)).ToListAsync(); //// Get books where the title or description contains the keyword

        }
        // Basic CRUD
        public async Task<List<Author>> GetAllAsync() => await _context.Authors.ToListAsync();


        public async Task<Author?> GetbyIdAsync(int id)
        {

            return await _context.Authors.FindAsync(id);
        }


        public async Task<Author> AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }


        public async Task<Author> UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return author;
        }



        public async Task<Author> DeleteAsync(Author author)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return author;
        }
        public async Task<List<Author>> GetAuthorWithBooksByIdAsync(string keyword)
        {
            return await _context.Authors
                 .Include(s => s.Books)
                 .Where(N => N.Name.Contains(keyword))
                 .ToListAsync();
        }


    }
}
