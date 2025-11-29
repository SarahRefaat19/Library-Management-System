using AutoMapper;
using LibrarySystem.BusnissLogic.Dtos.BookDtos;
using LibrarySystem.BusnissLogic.Dtos.CategoriesDtos;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.BusnissLogic.Mapping
{
    public class CategoriesProfile :Profile
    {
        public CategoriesProfile()
        {
            CreateMap<Category, ReadCategoriesDto>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CategoryDescription, opt => opt.MapFrom(src => src.Description));
            CreateMap<Category, AddCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoriesDto>().ReverseMap();
            CreateMap<Category, CategoryWithBooksDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));
        }
    }
}
