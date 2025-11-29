using AutoMapper.Execution;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.ReposInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LibrarySystem.DataAccessLayer.Repositories
{
    public class LoanRepository: IGenericRepository<Loan> , ILoanRepository
    {
        public LibraryDbContext _context { get; set; }
        public LoanRepository(LibraryDbContext Context)
        {
            _context = Context;
        
        }
        public  async Task<List<Loan>> GetAllAsync()
        {
            return await _context.Loans.Include(l=>l.Book).Include(l=>l.Member).ToListAsync();
        }

        public async Task<Loan?> GetbyIdAsync(int id)
        {
            return await _context.Loans.Include(l => l.Book).Include(l => l.Member).FirstOrDefaultAsync(l => l.Id == id);
        }
        public async Task<Loan> AddAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
            return loan; 
        }

        public async Task<Loan> UpdateAsync(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
            return loan; 
        }

        public async Task<Loan> DeleteAsync(Loan loan)
        {
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            return loan;
        }


        public async  Task<IEnumerable<Loan>> GetLoansForMemberAsync(string id)
        {
            return await _context.Loans.Include(l => l.Book)
                .Include(l => l.Member)
                .Where(l => l.MemberId == id)
                .ToListAsync();

        }
       public async Task<IEnumerable<Loan>> GetLateLoans()
        {
            return await _context.Loans
               .Include(l => l.Book)
               .Include(l => l.Member)
               .Where(l => l.BorrowDate < DateTime.Now && l.ReturnDate == null)
               .ToListAsync();
        }
      public async  Task<bool> IsBookLoaned(int id)
        {
            return await _context.Loans
               .AnyAsync(l => l.BookId == id && l.ReturnDate == null);
        }
      public async  Task MarkAsReturnedAsync(int id, DateTime ReturnedDate)
        {
            var Loan = await _context.Loans.Include(l=>l.Book).FirstOrDefaultAsync(M => M.Id == id);
            if (Loan != null)
            {
                Loan.ReturnDate = ReturnedDate;
                Loan.IsReturned = true;
                Loan.Book.Status = BookStatus.Available;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ChangeStatusAsync(int id, LoanStatus status)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan == null) return false;

            loan.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }



    }
}
