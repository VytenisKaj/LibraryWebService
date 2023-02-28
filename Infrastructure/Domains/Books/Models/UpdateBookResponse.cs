namespace Infrastructure.Domains.Books.Models
{
    public class UpdateBookResponse : Response
    {
        public bool Found { get; private set; }
        public BookRequest Book { get; private set; }
        public UpdateBookResponse(bool success, string message, bool found, BookRequest book) : base(success, message)
        {
            Found = found;
            Book = book;
        }

        public UpdateBookResponse() : this(true, null!, true, null!)
        {

        }

        public UpdateBookResponse(string message, bool found) : this(false, message, found, null!)
        {

        }
    }
}
