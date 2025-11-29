using AutoMapper;
using LibrarySystem.BusnissLogic.Dtos.UserDtos;
using LibrarySystem.BusnissLogic.Dtos.MemberDtos;

using LibrarySystem.Domain.Entities;

namespace LibrarySystem.BusnissLogic.Mapping
{
    public class MemberProfile :Profile
    {
        public MemberProfile()
        {

            CreateMap<Member, MemberReadDto>();
            CreateMap<MemberCreateDto, Member>()
                .ForMember(dst => dst.UserName, dst => dst.MapFrom(src => src.Email));

            CreateMap<MemberUpdateDto, Member>()
        .ForAllMembers(option => option.Condition((src, dest, srcMember) => srcMember != null)); // تيجي تنقل القيم، لو القيمة في الـ DTO كانت null، ما تحدثهاش في الكيان.


        }
    }
}

