using AutoMapper;
using dotnetcore_rpg.Dtos.Character;
using dotnetcore_rpg.Models;

namespace dotnetcore_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
            
        }
        private static List<Character> characters= new List<Character>{
            new Character() , 
            new Character{ ID = 1 , Name = "Sam"}
        };
        public  async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.ID = characters.Max(c => c.ID) + 1;
            characters.Add(character);
            response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return response;
        }

        public  async Task<ServiceResponse<List<GetCharacterDto>>> GetAll()
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return response;
        }

        public  async Task<ServiceResponse<GetCharacterDto>> GetCharacterByID(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            var character = characters.FirstOrDefault(c => c.ID == id);
            response.Data = _mapper.Map<GetCharacterDto>(character);
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdatedCharacterDto updatedCharacter)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            var character = characters.FirstOrDefault(c => c.ID == updatedCharacter.ID);
            character.Name = updatedCharacter.Name;
            character.Hitpoints = updatedCharacter.Hitpoints;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Strength = updatedCharacter.Strength;
            character.Defense = updatedCharacter.Defense;
            character.Class = updatedCharacter.Class;
            response.Data = _mapper.Map<GetCharacterDto>(character);
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> Delete(int id)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var character = characters.FirstOrDefault(c => c.ID == id);
            characters.Remove(character);
            response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return response;
        }
    }
}
        