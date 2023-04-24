using AutoMapper;
using Infrastructure.Domains.Authors.Repositories;
using Infrastructure.Domains.Books.Models;
using Infrastructure.Domains.Books.Repositories;
using Infrastructure.Domains.Users.Models;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Domains.Books.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository,  IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<CreateBookResponse> CreateBookAsync(BookRequest createRequest)
        {
            var author = _authorRepository.GetAuthor(createRequest.AuthorId);
            if (author == null)
            {
                return new CreateBookResponse($"Author with id {createRequest.AuthorId} doesn't exits");
            }

            var book = _mapper.Map<Book>(createRequest);

            if (!createRequest.IsAvailable)
            {
                var client = _httpClientFactory.CreateClient("UsersAPI");
                try
                {
                    await client.GetFromJsonAsync<User>($"api/Users/{createRequest.ReaderId}");
                }
                catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return new CreateBookResponse(ex.Message);
                }
                book.ReaderId = createRequest.ReaderId;
            }

            book.Author = author;
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

        public async Task<GetBookResponse> GetBookAsync(int id)
        {
            var book = _bookRepository.GetBook(id);
            if (book == null)
            {
                return new GetBookResponse($"Book with id {id} doesn't exits");
            }

            var mappedBook = _mapper.Map<BookResponse>(book);

            if (!book.IsAvailable) {
                var client = _httpClientFactory.CreateClient("UsersAPI");
                mappedBook.Reader = await client.GetFromJsonAsync<User>($"api/Users/{book.ReaderId}");
            }

            return new GetBookResponse(mappedBook);
            
        }

        public async Task<UpdateBookResponse> UpdateBookAsync(int id, BookRequest updateRequest)
        {
            var book = _bookRepository.GetBook(id);
            if (book == null)
            {
                return new UpdateBookResponse($"Book with id {id} doesn't exits", false);
            }

            if (!updateRequest.IsAvailable)
            {
                var client = _httpClientFactory.CreateClient("UsersAPI");
                try
                {
                    await client.GetFromJsonAsync<User>($"api/Users/{updateRequest.ReaderId}");
                }
                catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return new UpdateBookResponse(ex.Message, true);
                }
                book.ReaderId = updateRequest.ReaderId;
            }

            var author = _authorRepository.GetAuthor(updateRequest.AuthorId);
            if (author == null)
            {
                return new UpdateBookResponse($"Author with id {updateRequest.AuthorId} doesn't exits", false);
            }
            book.Author = author;
            book.Title = updateRequest.Title;
            book.Description = updateRequest.Description;
            book.CreatedDate = updateRequest.CreatedDate;
            book.Isbn = updateRequest.Isbn;
            book.IsAvailable = updateRequest.IsAvailable;
            book.UnavailableUntil = updateRequest.UnavailableUntil ?? DateTime.Now;
            _bookRepository.UpdateBook();
            return new UpdateBookResponse(updateRequest);
        }

        public async Task<CreateBookResponse> CreateBookWithUserAsync(CreateBookAndUserRequest request)
        {
            var author = _authorRepository.GetAuthor(request.AuthorId);
            if (author == null)
            {
                return new CreateBookResponse($"Author with id {request.AuthorId} doesn't exits");
            }

            var book = _mapper.Map<Book>(request);
            book.IsAvailable = false;

            var client = _httpClientFactory.CreateClient("UsersAPI");
            try
            {
                var response = await client.PostAsync($"api/Users", new StringContent(JsonSerializer.Serialize(request.Reader), Encoding.UTF8, "application/json"));
                
                if (!response.IsSuccessStatusCode)
                {
                    return new CreateBookResponse(await response.GetErrorsFromHttpResponse());
                }

                book.ReaderId = response.CreatedObjectId();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound || ex.StatusCode == HttpStatusCode.BadRequest)
            {
                return new CreateBookResponse(ex.Message);
            }

            book.Author = author;
            var newBook = _bookRepository.CreateBook(book);
            return new CreateBookResponse(newBook);
        }
    }
}
