using LibrarySystem.Domain.Entities;

namespace LibrarySystem.DataAccessLayer.DbSeeder
{
    public class BooksSeeder
    {
        private readonly LibraryDbContext _context;

        public BooksSeeder(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task SeedBooksAsync()
        {
            if (!_context.Books.Any())
            {
                var SoftwareEngineeringCategory = _context.Categories.FirstOrDefault(c => c.Name == "Software Engineering");
                var ProgrammingCategory = _context.Categories.FirstOrDefault(c => c.Name == "Programming");
                var AIandMachineLearningCategory = _context.Categories.FirstOrDefault(c => c.Name == "AI & Machine Learning");
                var NetworkingCategory = _context.Categories.FirstOrDefault(c => c.Name == "Networking");
                var DatabasesCategory = _context.Categories.FirstOrDefault(c => c.Name == "DataBases");


                if (SoftwareEngineeringCategory == null) throw new Exception("Category 'Software Engineering' not found");
                if (ProgrammingCategory == null) throw new Exception("Category 'Programming' not found");

                // -----------------------------------------------------------------------------------------


                var CleanCodeAuthor = _context.Authors.FirstOrDefault(a => a.Name == "Robert C. Martin");
                var JonSkeetAuthor = _context.Authors.FirstOrDefault(a => a.Name == "Jon Skeet");

                if (CleanCodeAuthor == null)
                    throw new Exception("Author 'Robert C. Martin' not found in database");

                if (JonSkeetAuthor == null)
                    throw new Exception("Author 'Jon Skeet' not found in database");

                _context.Books.AddRange(
                new Book
                {
                    Title = "CleanCode",
                    AuthorId = CleanCodeAuthor.AuthorId,
                    CategoryId = SoftwareEngineeringCategory.Id,
                    ImageUrl = "wwwroot/Uploads/CLean Code Book.webp"
                },
                new Book
                {
                    Title = "C# In Depth",
                    AuthorId = JonSkeetAuthor.AuthorId,
                    CategoryId = ProgrammingCategory.Id,
                    ImageUrl = "wwwroot/Uploads/C# In depth.webp"
                }
            );

                await _context.SaveChangesAsync();
            }
        }
    }
}
