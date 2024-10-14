using Microsoft.EntityFrameworkCore;
using libraryWeb.Data;

namespace libraryWeb
{

    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext _context;

        public AuthorRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAuthorAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            _context.Authors.Update(author);
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await GetAuthorByIdAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }
        }
    }

}
