using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Domain.Entities
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }  

        [Required]
        public string? Name { get; set; }   

        [MaxLength(1000)]
        public string? Bio { get; set; }   

        public DateTime BirthDate { get; set; } 
        public string? Nationality { get; set; } 

        //============================
        //Relationships
        public List<Book> Books { get; set; } = new List<Book>();



    }
}
