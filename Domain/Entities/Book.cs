
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Domain.Entities
{


    public enum BookStatus
    {
        Available,  
        Borrowed,   
        Reserved,   
        Lost,       

    }

public class Book
    {
        
        public int Id { get; set; }

        public string Title { get; set; } = "";
        public bool IsAvailable { get; set; } = true;
        public string? ISBN { get; set; } 

        public string? Description { get; set; }

        public BookStatus Status { get; set; }  
        public string ImageUrl { get; set; } = " ";

        [NotMapped]
        public IFormFile? ImageFile { get; set; } 

        //====================================================================
        // One to many relation 
            public int AuthorId { get; set; }
             public Author? Author { get; set; }
             public ICollection<Loan> Loans { get; set; } = new List<Loan>();



        //=================================================
        //one to one Relation 
        public int CategoryId { get; set; }
        public Category? Category { get; set; }



    }
}
