using libraryWeb;

public interface IUnitOfWork
{
    IBookRepository Books { get; }
    IAuthorRepository Authors { get; }
    Task<int> CompleteAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryContext _context;

    public UnitOfWork(LibraryContext context)
    {
        _context = context;
        Books = new BookRepository(context);
        Authors = new AuthorRepository(context);
    }

    public IBookRepository Books { get; private set; }
    public IAuthorRepository Authors { get; private set; }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
