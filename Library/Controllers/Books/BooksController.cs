using Infrastructure.Domains.Books.Models;
using Infrastructure.Domains.Books.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Controllers.Books
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {

        private readonly IBookService _bookService;

        public BooksController(IBookService service)
        {
            _bookService = service;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Returns all books")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public IActionResult GetAllBooks()
        {
            return Ok(_bookService.GetAllBooks());
        }


        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Returns book by id")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetBook(int id)
        {
            var bookResponse = await _bookService.GetBookAsync(id);

            if (!bookResponse.Success)
            {
                return NotFound(new { message = bookResponse.Message });
            }
            return Ok(bookResponse.Book);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates new book from request")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> CreateBook([FromBody] BookRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookResponse = await _bookService.CreateBookAsync(request);

            if (!bookResponse.Success)
            {
                return BadRequest(new { message = bookResponse.Message });
            }

            return CreatedAtAction(nameof(GetBook), new { id = bookResponse.Book?.Id }, bookResponse.Book);
        }

        [HttpPost("User")]
        [SwaggerOperation(Summary = "Creates new book and new user from request")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> CreateBookAndUser([FromBody] CreateBookAndUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookResponse = await _bookService.CreateBookWithUserAsync(request);

            if (!bookResponse.Success)
            {
                return BadRequest(new { message = bookResponse.Message });
            }

            return CreatedAtAction(nameof(GetBook), new { id = bookResponse.Book?.Id }, bookResponse.Book);
        }


        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updates book with id")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> UpdateBook([FromBody] BookRequest request, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookResponse = await _bookService.UpdateBookAsync(id, request);

            if (!bookResponse.Success)
            {
                if (!bookResponse.Found)
                {
                    return NotFound(new { message = bookResponse.Message });
                }
                return BadRequest(new { message = bookResponse.Message });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes book with id")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public IActionResult DeleteBook(int id)
        {
            var bookResponse = _bookService.DeleteBook(id);

            if (!bookResponse.Success)
            {
                if (!bookResponse.Found)
                {
                    return NotFound(new { message = bookResponse.Message });
                }
                return BadRequest(new { message = bookResponse.Message });
            }
            return Ok();
        }
    }
}