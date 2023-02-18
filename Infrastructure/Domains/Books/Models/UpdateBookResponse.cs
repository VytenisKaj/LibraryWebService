namespace Infrastructure.Domains.Books.Models
{
    public class UpdateBookResponse : Response
    {
        public bool Found { get; private set; }
        public UpdateBookResponse(bool success, string message, bool found) : base(success, message)
        {
            Found = found;
        }

        public UpdateBookResponse() : this(true, null!, true)
        {

        }

        public UpdateBookResponse(string message, bool found) : this(false, message, found)
        {

        }
    }
}
