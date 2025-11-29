using LibrarySystem.DataAccessLayer;
using LibrarySystem.DataAccessLayer.Repositories;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.ReposInterfaces;

namespace LibrarySystem.BusnissLogic.UnitOfWork
{
    public class UnitOfWork : IUnitofWorks, IDisposable
    {
        private readonly LibraryDbContext _context;

 
        public IGenericRepository<Category> Categories { get; }
        public IGenericRepository<Author> author { get; }
       public  IGenericRepository<Loan> Loans { get; }
        public ILoanRepository _Loans { get; }
        public ICategoryRepository _Categories { get; }

        public IbookRepository Books { get; }
        public IAuthorRepository Authors { get; }
      public  IUserRepository _User { get; }
      public  IMemberRepository _Member { get; }
     public   ILibrarianRepository _Librarian { get; }

        public UnitOfWork(LibraryDbContext context, 
            IGenericRepository<Category> categories,
            IGenericRepository<Author> Author,
            IbookRepository books,
            ICategoryRepository category,
            IAuthorRepository _Author,
            IGenericRepository<Loan> loans,
            ILoanRepository _loans,
            IUserRepository UserRepository,
            IMemberRepository memberRepository,
            ILibrarianRepository librarianRepository
            )
        {
            _context = context;
            Categories = categories;
            Books = books;
            _Categories= category ;
            Authors= _Author;
            _Loans = _loans;
            _User = UserRepository;
            _Member = memberRepository;
            author= Author ;
            _Librarian = librarianRepository;
        }

        
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
