using LibrarySystem.BusnissLogic.Dtos.LoanDtos;
using LibrarySystem.BusnissLogic.ServicesClasses;
using LibrarySystem.BusnissLogic.ServicesInterfaces;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace LibrarySystem.Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly IUserService _userService;

        public LoanController(ILoanService loanService, IUserService userService)
        {
            _loanService = loanService;
            _userService = userService;
        }

        // Member or Librarian can create loan request
        [Authorize(Roles = "Member,Librarian,Admin")]
        [HttpPost("request")]
        public async Task<IActionResult> RequestLoan([FromBody] LoanAddDto loanAddDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addedLoan = await _loanService.AddLoanAsync(loanAddDto);
            return CreatedAtAction(nameof(GetLoanById), new { id = addedLoan.Id }, addedLoan);
        }

        // Librarian approves loan
        [Authorize(Roles = "Admin,Librarian")]
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveLoan(int id)
        {
            var result = await _loanService.ChangeLoanStatusAsync(id, LoanStatus.Approved);
            if (!result) return NotFound("Loan not found or cannot be approved.");
            return Ok("Loan approved successfully.");
        }

        // Librarian rejects loan
        [Authorize(Roles = "Admin,Librarian")]
        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectLoan(int id)
        {
            var result = await _loanService.ChangeLoanStatusAsync(id, LoanStatus.Rejected);
            if (!result) return NotFound("Loan not found or cannot be rejected.");
            return Ok("Loan rejected successfully.");
        }

        // Return book (Member or Librarian)
        [Authorize(Roles = "Member,Librarian")]
        [HttpPut("{id}/return")]
        public async Task<IActionResult> ReturnBook(int id, [FromQuery] DateTime returnedDate)
        {
            await _loanService.MarkAsReturnedAsync(id, returnedDate);
            return Ok("Book returned successfully.");
        }

        // Get loan by ID
        [Authorize(Roles = "Admin,Librarian")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoanById(int id)
        {
            var loan = await _loanService.GetLoanByIdAsync(id);
            if (loan == null) return NotFound();
            return Ok(loan);
        }

        // Get loans for a member
        [Authorize(Roles = "Member,Librarian,Admin")]
        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetLoansForMember(string memberId)
        {
            var loggedUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (loggedUserId == null) return Unauthorized();

            if (User.IsInRole("Member") && loggedUserId != memberId)
                return Forbid();

            var loans = await _loanService.GetLoansForMemberAsync(memberId);
            return Ok(loans);
        }

        // Get all loans (Admin & Librarian)
        [Authorize(Roles = "Admin,Librarian")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllLoans()
        {
            var loans = await _loanService.GetAllLoansAsync();
            return Ok(loans);
        }

        // Get late loans (Admin & Librarian)
        [Authorize(Roles = "Admin,Librarian")]
        [HttpGet("late")]
        public async Task<IActionResult> GetLateLoans()
        {
            var loans = await _loanService.GetLateLoansAsync();
            return Ok(loans);
        }

        //  Member profile with borrowed books
        [Authorize(Roles = "Member")]
        [HttpGet("myprofile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var user = await _userService.GetUserByIdAsync(userId);
            var loans = await _loanService.GetLoansForMemberAsync(userId);

            return Ok(new
            {
                Name = user.Name,
                Email = user.Email,
                BorrowedBooks = loans
            });
        }
    }
}