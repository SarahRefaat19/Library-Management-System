using AutoMapper;
using LibrarySystem.Domain.Entities; 
using LibrarySystem.BusnissLogic.Dtos.BookDtos; 

namespace LibrarySystem.BusnissLogic.Mapping
{
    public class BookProfile :Profile
    {
        public BookProfile()
        {
            CreateMap<Book, ReadBookDto>().ReverseMap();


            CreateMap<Book, ListBookDto>().ReverseMap();


            CreateMap<Book, CreateBookDto>().ReverseMap();


            CreateMap<Book, UpdateBookDto>().ReverseMap();

        }
    }
}
