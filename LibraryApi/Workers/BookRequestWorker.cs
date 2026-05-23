using LibraryApi.Repositories;
using LibraryApi.Services;

public class BookRequestWorker : BackgroundService
{
  private readonly IBookRequestQueue _queue;
  private readonly ILogger<BookRequestWorker> _logger;
  private readonly IServiceScopeFactory _scopeFactory;

  public BookRequestWorker(IBookRequestQueue queue, ILogger<BookRequestWorker> logger, IServiceScopeFactory scopeFactory)
  {
    _queue = queue;
    _logger = logger;
    _scopeFactory = scopeFactory;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      var request = _queue.Dequeue();
      if (request is not null)
      {
        using var scope = _scopeFactory.CreateScope();
        var bookRepository = scope.ServiceProvider.GetRequiredService<IBookRepository>();

        var book = await bookRepository.GetByIdAsync(request.BookId);

        if (book == null)
        {
          _logger.LogWarning("Book with ID {BookId} not found.", request.BookId);
        }
        else if (book.Stock <= 0)
        {
          _logger.LogWarning("Book '{Title}' is out of stock.", book.Title);
        }
        else
        {
          book.Stock -= 1;
          await bookRepository.UpdateAsync(book.Id, book);
          _logger.LogInformation("Book '{Title}' borrowed by {UserId}. Remaining stock: {Stock}.", book.Title, request.UserId, book.Stock);
        }
      }

      await Task.Delay(500, stoppingToken);
    }
  }
}