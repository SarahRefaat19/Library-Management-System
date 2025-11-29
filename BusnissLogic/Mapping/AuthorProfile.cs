using AutoMapper;
using LibrarySystem.BusnissLogic.Dtos.AuthorDtos;
using LibrarySystem.Domain.Entities;


namespace LibrarySystem.BusnissLogic.Mapping
{
    public class AuthorProfile :Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorCreateDto>().ReverseMap();


            CreateMap<Author, AuthorReadDto>()
             .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name ?? ""))
                .ForMember(dest => dest.NumberOfBooks, opt => opt.MapFrom(src => src.Books != null ? src.Books.Count : 0));

            CreateMap<Author, AuthorReadWithBooksDto>()
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
               .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Name ?? ""))
    .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));


            CreateMap<Author, AuthorUpdateDto>().ReverseMap();


            CreateMap<Author, AuthorDeleteDto>().ReverseMap();
        }
    }
}
