namespace libraryWeb
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LName { get; set; }
        public DateTime Birthday { get; set; }
        public string Country { get; set; }
        public ICollection<Book> Books { get; set; } // Связь один ко многим


        //public List<Book> Books { get; set; } = new List<Book>();
    }
    
    
}
