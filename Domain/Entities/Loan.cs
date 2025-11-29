namespace LibrarySystem.Domain.Entities
{

    public enum WeekDay
    {
        saturday,
        sunday,
        monday,
        tuseday,
        wednesday,
        thursday,
        friday


    }
    public enum LoanStatus
    {
        Pending,
        Approved,
        Rejected,
        Returned
    }
    public class Loan
    {

        public int Id { get; set; }
        public string BorrowerName { get; set; } = "";
        public Book? BorrowedBook { get; set; }
        public WeekDay BorrowedDay { get; set; }
        public int LoanPeriod { get; set; } = 20; 
        public DateTime DueDate { get; set; }

        public DateTime BorrowDate { get; set; } = DateTime.Now;
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }

        public LoanStatus Status { get; set; } = LoanStatus.Pending;


        //=======================================
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public string MemberId { get; set; } = "";
        public Member? Member { get; set; }



    }
}
