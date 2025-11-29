using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using System.Linq.Expressions;

namespace LibrarySystem.Domain.ReposInterfaces
{
    public interface ILoanRepository :IGenericRepository<Loan>
    {
        Task <IEnumerable<Loan>> GetLoansForMemberAsync(string id);
        Task<IEnumerable<Loan>> GetLateLoans();
        Task <bool> IsBookLoaned(int id);
        Task MarkAsReturnedAsync(int id, DateTime ReturnedDate);
        Task <bool>ChangeStatusAsync(int id, LoanStatus status);


    }
}
