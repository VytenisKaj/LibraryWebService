using AutoMapper;
using Infrastructure.Domains.Books.Models;

namespace Library.Controllers.Books.Mappings
{
    public class BookMapping : Profile
    {
        public BookMapping()
        {
            CreateMap<Book, BookRequest>().ReverseMap();
        }
    }
}
