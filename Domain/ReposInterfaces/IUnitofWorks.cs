using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;

namespace LibrarySystem.Domain.ReposInterfaces
{
    public interface IUnitofWorks :IDisposable
    {
        IbookRepository Books { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Author> author { get; }
        IGenericRepository<Loan> Loans { get; }
        ILoanRepository  _Loans { get; }

        ICategoryRepository _Categories { get; }
        IAuthorRepository Authors { get; }
        IUserRepository _User { get; }
        IMemberRepository _Member { get; }
        ILibrarianRepository _Librarian { get; }

        Task<int> CompleteAsync();

    }
}
