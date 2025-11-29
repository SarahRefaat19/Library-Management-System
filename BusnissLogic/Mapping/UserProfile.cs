using AutoMapper;
using LibrarySystem.BusnissLogic.Dtos.UserDtos;
using LibrarySystem.Domain.Entities;


namespace LibrarySystem.BusnissLogic.Mapping
{
    public class UserProfile :Profile
    {
        public UserProfile ()
        {

            CreateMap<User,UserReadDto> ();
            CreateMap<UserCreateDto, User>()
                .ForMember(dst => dst.UserName, dst => dst.MapFrom(src => src.Email));

            CreateMap<UserUpdateDto,User >()
        .ForAllMembers(option => option.Condition((src, dest, srcMember) => srcMember != null)); // تيجي تنقل القيم، لو القيمة في الـ DTO كانت null، ما تحدثهاش في الكيان.


        }
    }
}
