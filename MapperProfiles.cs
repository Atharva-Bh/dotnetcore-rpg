using AutoMapper;
using dotnetcore_rpg.Dtos.Character;

namespace dotnetcore_rpg
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Character , GetCharacterDto>();
            CreateMap<AddCharacterDto , Character>();
        }
    }
}