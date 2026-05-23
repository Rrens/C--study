namespace LibraryApi.Services
{
  public class BookRequestQueue : IBookRequestQueue
  {
    private readonly Queue<BookBorrowRequest> _queue = new();
    private readonly object _lock = new object();
    private readonly int _maxCapacity;

    public BookRequestQueue(int maxCapacity = 100)
    {
      _maxCapacity = maxCapacity;
    }

    public bool Enqueue(Guid bookId, string userId)
    {
      lock (_lock)
      {
        if (_queue.Count >= _maxCapacity) return false;

        _queue.Enqueue(new BookBorrowRequest(bookId, userId));
        return true;
      }
    }

    public BookBorrowRequest? Dequeue()
    {
      lock (_lock)
      {
        if (_queue.Count == 0) return null;

        return _queue.Dequeue();
      }
    }
  }
}