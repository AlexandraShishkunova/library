using libraryWeb;
using libraryWeb.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    //private readonly IBookRepository _bookRepository;
    //public BooksController(IBookRepository bookRepository)
    //{
    //    _bookRepository = bookRepository;
    //}

    public BooksController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    //Получение всех книг
    [HttpGet]
    public async Task<ActionResult> GetAllBooks()
    {
        var books = await _unitOfWork.Books.GetAllBooksAsync();
        return Ok(books);
    }

    //Получение определенной книги по ее id
    [HttpGet("id/{id:int}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var book = await _unitOfWork.Books.GetBookByIdAsync(id);
        if (book == null)
            return NotFound();
        return Ok(book);
    }

    //Получение книги по её ISBN
    [HttpGet("{ISBN}")]
    public async Task<IActionResult> GetBook(string isbn)
    {
        var book = await _unitOfWork.Books.GetBookByISBNAsync(isbn);
        if(book==null)
            return NotFound();
        return Ok(book);
    }
    //Выдача книги на руки пользователю
    //Возможность добавления изображения к книге и его хранение


    //Добавить новую книгу
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] Book book)
    {
        await _unitOfWork.Books.AddBookAsync(book);
        await _unitOfWork.CompleteAsync();
        return Ok(book);
        //return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    //Изменение информации о существующей книге
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
    {
        if (id != book.Id)
            return BadRequest();

        await _unitOfWork.Books.UpdateBookAsync(book);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }

    //Удаление книги
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _unitOfWork.Books.DeleteBookAsync(id);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }
}
