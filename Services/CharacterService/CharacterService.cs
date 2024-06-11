using AutoMapper;
using dotnetcore_rpg.Data;
using dotnetcore_rpg.Dtos.Character;
using Microsoft.EntityFrameworkCore;

namespace dotnetcore_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper , DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public  async Task<ServiceResponse<List<GetCharacterDto>>> 
        AddCharacter(AddCharacterDto newCharacter)
        {
            var response = 
            new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            response.Data = _context.Characters.
            Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return response;
        }

        public  async Task<ServiceResponse<List<GetCharacterDto>>> GetAll()
        {
            var response = 
            new ServiceResponse<List<GetCharacterDto>>();
           // response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
           response.Data = await _context.Characters.
           Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
           return response;
        }

        public  async Task<ServiceResponse<GetCharacterDto>> GetCharacterByID(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.ID == id);
            if(character is null)
            {
                response.Success = false;
                response.Message = $"The character with ID {id} has  not been found!";
                return response;
            }
            response.Data = _mapper.Map<GetCharacterDto>(character);
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdatedCharacterDto updatedCharacter)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try{
            var character = await _context.Characters.
            FirstAsync(c => c.ID == updatedCharacter.ID);
            character.Name = updatedCharacter.Name;
            character.Hitpoints = updatedCharacter.Hitpoints;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Strength = updatedCharacter.Strength;
            character.Defense = updatedCharacter.Defense;
            character.Class = updatedCharacter.Class;
            _context.Characters.Update(character);
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetCharacterDto>(character);
            }catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> Delete(int id)
        {
            var response = 
            new ServiceResponse<List<GetCharacterDto>>();
            try{
            var character = await _context.Characters.FirstAsync(c => c.ID == id);
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            response.Data = _context.Characters.
            Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
            return response;
        }
    }
}
        