using LibrarySystem.BusnissLogic.Dtos.LoanDtos;
using LibrarySystem.Domain.Entities;
using System.Linq.Expressions;

namespace LibrarySystem.BusnissLogic.ServicesInterfaces
{
   
        public interface ILoanService
        {
            Task<IEnumerable<LoanReadDto>> GetAllLoansAsync();

        Task<LoanReadDto?> GetLoanByIdAsync(int id);
            Task<LoanReadDto> AddLoanAsync(LoanAddDto loanAddDto);
            Task<LoanReadDto?> UpdateLoanAsync(int id, LoanUpdateDto loanUpdateDto);
            Task<IEnumerable<LoanReadDto>> GetLoansForMemberAsync(string memberId);
            Task<IEnumerable<LoanReadDto>> GetLateLoansAsync();
            Task MarkAsReturnedAsync(int id, DateTime returnedDate);
        Task <bool> ChangeLoanStatusAsync(int id, LoanStatus status);
        }

}

