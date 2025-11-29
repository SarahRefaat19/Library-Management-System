using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LibrarySystem.DataAccessLayer
{
    public class LibraryDbContext : IdentityDbContext<User>
    {
          public LibraryDbContext(DbContextOptions<LibraryDbContext> options ):base(options) { }
            // DB SETS Defenetions 
        public DbSet<Book> Books { get; set; }
        public DbSet<User> LibraryUsers { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Author> Authors  { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>()
             .HasOne(b => b.Author)
             .WithMany(b => b.Books)
             .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId);



            modelBuilder.Entity<Loan>()
          .HasOne(l => l.Book)
          .WithMany(b => b.Loans)
         .HasForeignKey(l => l.BookId);

            modelBuilder.Entity<Member>()
           .Property(m => m.FineBalance)
           .HasColumnType("decimal(18,2)");
        }


    }
}
