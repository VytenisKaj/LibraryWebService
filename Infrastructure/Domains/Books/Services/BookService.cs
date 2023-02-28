using AutoMapper;
using Infrastructure.Domains.Books.Models;
using Infrastructure.Domains.Books.Repositories;

namespace Infrastructure.Domains.Books.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public CreateBookResponse CreateBook(BookRequest createRequest)
        {
            var book = _mapper.Map<Book>(createRequest);
            var newBook = _bookRepository.CreateBook(book);
            return new CreateBookResponse(newBook);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public DeleteBookResponse DeleteBook(int id)
        {
            var book = _bookRepository.GetBook(id);
            if (book == null)
            {
                return new DeleteBookResponse($"Book with id {id} doesn't exits", false);
            }
            _bookRepository.DeleteBook(book);
            return new DeleteBookResponse();
        }

        public GetBookResponse GetBook(int id)
        {
            var book = _bookRepository.GetBook(id);
            if (book == null)
            {
                return new GetBookResponse($"Book with id {id} doesn't exits");
            }
            return new GetBookResponse(_mapper.Map<BookRequest>(book));
            
        }

        public UpdateBookResponse UpdateBook(int id, BookRequest updateRequest)
        {
            var book = _bookRepository.GetBook(id);
            if (book == null)
            {
                return new UpdateBookResponse($"Book with id {id} doesn't exits", false);
            }
            book.Author = updateRequest.Author;
            book.Title = updateRequest.Title;
            book.Description = updateRequest.Description;
            book.CreatedDate = updateRequest.CreatedDate;
            book.Isbn = updateRequest.Isbn;
            book.IsAvailable = updateRequest.IsAvailable;
            book.UnavailableUntil = updateRequest.UnavailableUntil;
            _bookRepository.UpdateBook();
            return new UpdateBookResponse(true, null!, true, updateRequest);
        }
    }
}
