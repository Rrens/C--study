namespace LibraryApi.Services
{
    public interface IBookRequestQueue
    {
        bool Enqueue(Guid bookId, string userId);
        BookBorrowRequest? Dequeue();
    }

    public record BookBorrowRequest(Guid BookId, string UserId);
}
