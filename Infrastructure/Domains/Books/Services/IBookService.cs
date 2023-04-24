using Infrastructure.Domains.Books.Models;

namespace Infrastructure.Domains.Books.Services
{
    public interface IBookService
    {
        Task<CreateBookResponse> CreateBookAsync(BookRequest createRequest);

        Task<GetBookResponse> GetBookAsync(int id);

        IEnumerable<Book> GetAllBooks();

        Task<UpdateBookResponse> UpdateBookAsync(int id, BookRequest updateRequest);

        DeleteBookResponse DeleteBook(int id);
        Task<CreateBookResponse> CreateBookWithUserAsync(CreateBookAndUserRequest request);
    }
}
