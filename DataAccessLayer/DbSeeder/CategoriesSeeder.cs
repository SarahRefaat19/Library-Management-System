using LibrarySystem.Domain.Entities;

namespace LibrarySystem.DataAccessLayer.DbSeeder
{
    public class CategoriesSeeder
    {

        private readonly LibraryDbContext _context;

        public CategoriesSeeder(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task SeedCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.AddRange(
                  new Category { Name = "Programming" },
                    new Category { Name = "AI & Machine Learning" },
                    new Category { Name = "Web Development" },
                    new Category { Name = "Mobile Development" },
                    new Category { Name = "Networking" },
                    new Category { Name = "Cyber Security" },
                    new Category { Name = "DataBases" },
                  new Category { Name = "Software Engineering" }


                );

                await _context.SaveChangesAsync();
            }
        }
    }
}
