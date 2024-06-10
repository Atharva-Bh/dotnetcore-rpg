using dotnetcore_rpg.Dtos.Character;
using dotnetcore_rpg.Models;

namespace dotnetcore_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAll();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterByID(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);

    }
}