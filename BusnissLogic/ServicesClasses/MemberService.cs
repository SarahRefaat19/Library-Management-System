using AutoMapper;
using LibrarySystem.BusnissLogic.Dtos.MemberDtos;
using LibrarySystem.BusnissLogic.ServicesInterfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.ReposInterfaces;


   
    namespace LibrarySystem.BusnissLogic.ServicesClasses
    {
        public class MemberService : IMemberService
        {
            private readonly IMemberRepository _memberRepository;
            private readonly IMapper _mapper;

            public MemberService(IMemberRepository memberRepository, IMapper mapper)
            {
                _memberRepository = memberRepository;
                _mapper = mapper;
            }

            public async Task<MemberReadDto?> GetByIdAsync(string id)
            {
                var user = await _memberRepository.GetByIdAsync(id);
                if (user == null)
                    return null;

                // نحول الـ User إلى MemberReadDto
                var memberDto = _mapper.Map<MemberReadDto>(user);
                return memberDto;
            }

            public async Task<IEnumerable<MemberReadDto>> GetAllMembersAsync()
            {
                var members = await _memberRepository.GetAllMembersAsync();
                return _mapper.Map<IEnumerable<MemberReadDto>>(members);
            }

            public async Task<List<MemberReadDto>> GetActiveMembersAsync()
            {
                var activeMembers = await _memberRepository.GetActiveMembersAsync();
                return _mapper.Map<List<MemberReadDto>>(activeMembers);
            }

            public async Task UpdateFineAsync(string id, decimal amount)
            {
                await _memberRepository.UpdateFineAsync(id, amount);
            }

            public async Task CreateAsync(MemberCreateDto memberDto, string password)
            {
                var user = _mapper.Map<User>(memberDto);
                await _memberRepository.CreateAsync(user, password);
            }
            public async   Task AddToRoleAsync(string id , string role)
            {
                var user = _mapper.Map<User>(id);
                await _memberRepository.AddToRoleAsync(user , role);
            }


        }
    }

