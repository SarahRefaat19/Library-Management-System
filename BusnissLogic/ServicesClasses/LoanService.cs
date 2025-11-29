using AutoMapper;
using LibrarySystem.BusnissLogic.Dtos.LoanDtos;
using LibrarySystem.BusnissLogic.ServicesInterfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.ReposInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace LibrarySystem.BusnissLogic.ServicesClasses
{
    public class LoanService:ILoanService
    {
            private readonly ILoanRepository _loanRepository;
            private readonly IMapper _mapper;
            private readonly IbookRepository _bookRepository;
            private readonly UserManager<User> _userManager;


        public LoanService(ILoanRepository loanRepository, IMapper mapper, IbookRepository bookRepository, UserManager<User> userManager)
            {
                _loanRepository = loanRepository;
                _mapper = mapper;
                _bookRepository = bookRepository;
                _userManager = userManager;
            }

            public async Task<IEnumerable<LoanReadDto>> GetAllLoansAsync()
            {
                var loans = await _loanRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<LoanReadDto>>(loans);
            }

            public async Task<LoanReadDto?> GetLoanByIdAsync(int id)
            {
                var loan = await _loanRepository.GetbyIdAsync(id);
                return _mapper.Map<LoanReadDto?>(loan);
            }

        public async Task<LoanReadDto> AddLoanAsync(LoanAddDto loanAddDto)
        {
            var loan = _mapper.Map<Loan>(loanAddDto);

            var member = await _userManager.Users.OfType<Member>()
                .FirstOrDefaultAsync(m => m.Id == loan.MemberId);
            if (member == null)
                throw new InvalidOperationException("Member not found.");

            var book = await _bookRepository.GetbyIdAsync(loan.BookId);
            if (book == null)
                throw new InvalidOperationException("Book not found.");

            loan.Status = LoanStatus.Pending;
            loan.BorrowDate = DateTime.Now;

            await _loanRepository.AddAsync(loan);
            return _mapper.Map<LoanReadDto>(loan);
        }


        public async Task<LoanReadDto?> UpdateLoanAsync(int id, LoanUpdateDto loanUpdateDto)
            {
            var existingLoan = await _loanRepository.GetbyIdAsync(id);
            if (existingLoan == null) return null;

            _mapper.Map(loanUpdateDto, existingLoan);
            await _loanRepository.UpdateAsync(existingLoan);

            return _mapper.Map<LoanReadDto>(existingLoan);
        }

            

            public async Task<IEnumerable<LoanReadDto>> GetLoansForMemberAsync(string memberId)
            {
                var loans = await _loanRepository.GetLoansForMemberAsync(memberId);
                return _mapper.Map<IEnumerable<LoanReadDto>>(loans);
            }

            public async Task<IEnumerable<LoanReadDto>> GetLateLoansAsync()
            {
                var loans = await _loanRepository.GetLateLoans();
                return _mapper.Map<IEnumerable<LoanReadDto>>(loans);
            }

        public async Task MarkAsReturnedAsync(int id, DateTime returnedDate)
        {

            var loan = await _loanRepository.GetbyIdAsync(id);
            if (loan == null) throw new Exception("Loan not found");

            loan.ReturnDate = returnedDate;

            var dueDate = loan.BorrowDate.AddDays(loan.LoanPeriod);

            if (dueDate < returnedDate)
            {
                int lateDays = (returnedDate - dueDate).Days;
                lateDays = Math.Max(0, lateDays);

                if (lateDays > 0)
                {
                    decimal finePerDay = 100;
                    decimal totalFine = lateDays * finePerDay;

                    var member = await _userManager.Users.OfType<Member>()
                        .FirstOrDefaultAsync(m => m.Id == loan.MemberId);

                    if (member != null)
                    {
                        member.FineBalance += totalFine;
                        await _userManager.UpdateAsync(member);
                    }
                }
            }

            var book = await _bookRepository.GetbyIdAsync(loan.BookId);
            if (book != null)
            {
                book.Status = BookStatus.Available;
                await _bookRepository.UpdateAsync(book);
            }

            loan.Status = LoanStatus.Returned;
            await _loanRepository.UpdateAsync(loan);


        }

        public async Task<bool> ChangeLoanStatusAsync(int id, LoanStatus status)
        {
            var loan = await _loanRepository.GetbyIdAsync(id);
            if (loan == null)
                return false;

            if (loan.Status == LoanStatus.Returned)
                throw new InvalidOperationException("Can't change status of a returned loan.");

            if (status == LoanStatus.Approved)
            {
                if (loan.Status != LoanStatus.Pending)
                    throw new InvalidOperationException("Only pending loans can be approved.");

                var book = await _bookRepository.GetbyIdAsync(loan.BookId);
                if (book.Status != BookStatus.Available)
                    throw new InvalidOperationException("Book is already borrowed.");

                loan.Status = LoanStatus.Approved;
                loan.BorrowDate = DateTime.Now;
                loan.DueDate = loan.BorrowDate.AddDays(loan.LoanPeriod);

                book.Status = BookStatus.Borrowed;
                await _bookRepository.UpdateAsync(book);
            }
            else if (status == LoanStatus.Rejected)
            {
                if (loan.Status != LoanStatus.Pending)
                    throw new InvalidOperationException("Only pending loans can be rejected.");

                loan.Status = LoanStatus.Rejected;
            }

            await _loanRepository.UpdateAsync(loan);
            return true;
        }
    }

    }





