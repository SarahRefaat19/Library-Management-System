namespace LibrarySystem.BusnissLogic.Dtos.LoanDtos
{
    public class LoanAddDto
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime LoanDate { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }
    }
}
