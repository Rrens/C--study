using LibraryApi.Models.Domain;
using LibraryApi.Models.DTos;
using LibraryApi.Repositories;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BooksController : ControllerBase
  {
    private readonly IBookRepository _bookRepository;
    private readonly IBookRequestQueue _queue;

    public BooksController(IBookRepository bookrepository, IBookRequestQueue queue)
    {
      _bookRepository = bookrepository;
      _queue = queue;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var booksDomain = await _bookRepository.GetAllAsync();
      var booksDto = booksDomain.Select(b => new BookDto
      {
        Id = b.Id,
        Title = b.Title,
        Author = b.Author,
        ISBN = b.ISBN,
        PublishedYear = b.PublishedYear,
        Stock = b.Stock
      });

      return Ok(booksDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookDto createBookDto)
    {
      var bookDomain = new Book
      {
        Title = createBookDto.Title,
        Author = createBookDto.Author,
        ISBN = createBookDto.ISBN,
        PublishedYear = createBookDto.PublishedYear,
        Stock = createBookDto.Stock
      };

      bookDomain = await _bookRepository.CreateAsync(bookDomain);

      return CreatedAtAction(nameof(GetById), new { id = bookDomain.Id }, bookDomain);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
      var book = await _bookRepository.GetByIdAsync(id);

      if (book == null) return NotFound();

      return Ok(book);
    }

    [HttpPost("{UserId:guid}/{BookId:guid}/borrow")]
    public async Task<IActionResult> BorrowBook([FromRoute] Guid UserId, [FromRoute] Guid BookId)
    {
      bool queued = _queue.Enqueue(BookId, UserId.ToString());

      if (!queued)
      {
        return StatusCode(503, "Book borrow request queue is full. Please try again later.");
      }

      return Accepted($"Book borrow request for Book ID {BookId} by User {UserId} has been queued for processing.");
    }
  }
}