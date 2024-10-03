using libraryWeb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AuthorsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // Получить всех авторов
    [HttpGet]
    public async Task<IActionResult> GetAuthors()
    {
        var authors = await _unitOfWork.Authors.GetAllAuthorsAsync();
        return Ok(authors);
    }

    // Получить автора по ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthor(int id)
    {
        var author = await _unitOfWork.Authors.GetAuthorByIdAsync(id);
        if (author == null)
            return NotFound();
        return Ok(author);
    }

    // Получить все книги по автору
    [HttpGet("{id}/books")]
    public async Task<IActionResult> GetBooksByAuthor(int id)
    {
        var author = await _unitOfWork.Authors.GetAuthorByIdAsync(id);
        if (author == null)
            return NotFound("Author not found");

        var books = await _unitOfWork.Books.GetBooksByAuthorIdAsync(id);
        return Ok(books);
    }

    // Добавить нового автора
    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] Author author)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _unitOfWork.Authors.AddAuthorAsync(author);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
    }

    // Обновить информацию о существующем авторе
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Author author)
    {
        if (id != author.Id)
            return BadRequest("Author ID mismatch");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingAuthor = await _unitOfWork.Authors.GetAuthorByIdAsync(id);
        if (existingAuthor == null)
            return NotFound();

        existingAuthor.Name = author.Name;
        existingAuthor.LName = author.LName;
        existingAuthor.Birthday = author.Birthday;
        existingAuthor.Country = author.Country;

        await _unitOfWork.Authors.UpdateAuthorAsync(existingAuthor);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    // Удалить автора
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var author = await _unitOfWork.Authors.GetAuthorByIdAsync(id);
        if (author == null)
            return NotFound();

        await _unitOfWork.Authors.DeleteAuthorAsync(id);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}
