using dotnetcore_rpg.Dtos.Character;
using dotnetcore_rpg.Models;

namespace dotnetcore_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters= new List<Character>{
            new Character() , 
            new Character{ ID = 1 , Name = "Sam"}
        };
        public  async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            characters.Add(newCharacter);
            response.Data = characters;
            return response;
        }

        public  async Task<ServiceResponse<List<Character>>> GetAll()
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            response.Data = characters;
            return response;
        }

        public  async Task<ServiceResponse<GetCharacterDto>> GetCharacterByID(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            var character = characters.FirstOrDefault(c => c.ID == id);
            response.Data = character;
            return response;
        }
    }
}
        