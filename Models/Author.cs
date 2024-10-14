using System.ComponentModel.DataAnnotations.Schema;

namespace libraryWeb
{
    [Table("author")]
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string Country { get; set; }
        public ICollection<Book> Books { get; set; } // Связь один ко многим


        //public List<Book> Books { get; set; } = new List<Book>();
    }
    
    
}
