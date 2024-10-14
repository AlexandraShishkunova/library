namespace libraryWeb.Data
{
        public interface IBookRepository
        {
            Task<IEnumerable<Book>> GetAllBooksAsync();
            Task<Book> GetBookByIdAsync(int id);
            Task<Book> GetBookByISBNAsync(string isbn);
            Task AddBookAsync(Book book);
            Task UpdateBookAsync(Book book);
            Task DeleteBookAsync(int id);
            //Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId);
        }
  
}
