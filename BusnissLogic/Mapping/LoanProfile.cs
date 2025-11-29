using AutoMapper;
using LibrarySystem.BusnissLogic.Dtos.LoanDtos;
using LibrarySystem.Domain.Entities;


namespace LibrarySystem.BusnissLogic.Mapping
{
    public class LoanProfile: Profile
    {
        public LoanProfile()
        {

            CreateMap<Loan, LoanReadDto>()
              .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book))
              .ForMember(dest => dest.Member, opt => opt.MapFrom(src => src.Member));

            CreateMap<LoanAddDto, Loan>().ReverseMap();

            CreateMap<LoanUpdateDto, Loan>()
              .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
