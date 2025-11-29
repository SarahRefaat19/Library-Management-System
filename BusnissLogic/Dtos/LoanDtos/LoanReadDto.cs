 using LibrarySystem.BusnissLogic.Dtos;
using LibrarySystem.BusnissLogic.Dtos.BookDtos;
using LibrarySystem.BusnissLogic.Dtos.MemberDtos;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.BusnissLogic.Dtos.LoanDtos
{
    public class LoanReadDto
    {
        [Required]
        public int Id { get; set; }
        public string BorrowerName { get; set; } = "";
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }

        public ReadBookDto? Book { get; set; }
        public MemberReadDto? Member { get; set; }

    }
}
