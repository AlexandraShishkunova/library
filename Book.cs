namespace libraryWeb
{
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public DateTime? TakenDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public byte[] Image { get; set; }
        //public bool IsAvailable { get; set; } = true;
    }
}
