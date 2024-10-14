using System.ComponentModel.DataAnnotations.Schema;

namespace libraryWeb
{
    [Table("book")]
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public DateTime? GetTime { get; set; }
        public DateTime? ReturnTime { get; set; }

        [ForeignKey("FK_book_author")]
        public int authorId { get; set; }
        public Author Author { get; set; }
        //public byte[] Image { get; set; }
        //public bool IsAvailable { get; set; } = true;
    }
}
