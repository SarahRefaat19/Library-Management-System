using LibrarySystem.Domain.Entities;

namespace LibrarySystem.DataAccessLayer.DbSeeder
{
    public class AuthorsSeeder
    {
        private readonly LibraryDbContext _context;

        public AuthorsSeeder(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task SeedAuthorsAsync()
        {
            if (!_context.Authors.Any())
            {
                _context.Authors.AddRange(
                    new Author { Name = "Robert C. Martin" },
                    new Author { Name = "Bjarne Stroustrup" },
                    new Author { Name = "Martin Fowler" },
                    new Author { Name = "Jon Skeet" },
                    new Author { Name = "Eric Freeman" },
                    new Author { Name = "Steve McConnell" },
                    new Author { Name = "Andrew Hunt" }
                );

                await _context.SaveChangesAsync();
            }
        }
    }
}
  