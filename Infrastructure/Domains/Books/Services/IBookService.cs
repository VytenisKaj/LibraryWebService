using Infrastructure.Domains.Books.Models;

namespace Infrastructure.Domains.Books.Services
{
    public interface IBookService
    {
        public Task<CreateBookResponse> CreateBookAsync(BookRequest createRequest);

        public Task<GetBookResponse> GetBookAsync(int id);

        public IEnumerable<Book> GetAllBooks();

        public UpdateBookResponse UpdateBook(int id, BookRequest updateRequest);

        public DeleteBookResponse DeleteBook(int id);

    }
}
