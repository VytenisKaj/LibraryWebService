namespace Infrastructure.Domains.Books.Models
{
    public class GetBookResponse : Response
    {
        public BookRequest? Book { get; private set; }
        public GetBookResponse(bool success, string message, BookRequest book) : base(success, message)
        {
            Book = book;
        }

        public GetBookResponse(string message) : this(false, message, null!)
        {

        }
        public GetBookResponse(BookRequest book) : this(true, null!, book)
        {

        }
    }
}
