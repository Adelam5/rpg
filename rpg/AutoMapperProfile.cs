using AutoMapper;
using rpg.Dtos.Character;

namespace rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            // CreateMap<UpdateCharacterDto, Character>();
        }
    }
}
