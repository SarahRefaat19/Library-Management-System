namespace LibrarySystem.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public string? Description { get; set; }
//========================================================

        //one to many 
        public List<Book> Books { get; set; } = new List<Book>();


    }


}

