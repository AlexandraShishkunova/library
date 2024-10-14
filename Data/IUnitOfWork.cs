namespace libraryWeb.Data
{
    public interface IUnitOfWork
    {
        IBookRepository Books { get; }
        IAuthorRepository Authors { get; }
        Task<int> CompleteAsync();
    }

}
