using Microsoft.AspNetCore.Identity;

namespace LibrarySystem.Domain.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = "";
        public string Role { get; set; } = ""; 

        public User() { }

        public User(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Member : User
    {
        public Member(string id, string name) : base(id, name)
        {
            Role = "Member";
        }

        public bool IsActive { get; set; } = true;
        public DateTime MembershipDate { get; set; } = DateTime.Now;
        public int MaxBorrowLimit { get; set; } = 5;
        public decimal FineBalance { get; set; } = 50;
        public List<Loan> Loans { get; set; } = new List<Loan>();

       
    }

    public class Librarian : User
    {
        public Librarian(string id, string name) : base(id, name)
        {
            Role = "Admin";
        }

        public string Department { get; set; } = "General";
        public DateTime HireDate { get; set; } = DateTime.Now;
    }
}
