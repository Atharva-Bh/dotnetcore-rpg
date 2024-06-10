using dotnetcore_rpg.Dtos.Character;
using Microsoft.AspNetCore.Mvc;
namespace dotnetcore_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
            
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _characterService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            return Ok(await _characterService.GetCharacterByID(id));
        }
        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddCharacterDto newcharacter)
        {
            return Ok(await _characterService.AddCharacter(newcharacter));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdatedCharacterDto updatedCharacter)
        {
            return Ok(await _characterService.UpdateCharacter(updatedCharacter));
        }
        [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                return Ok(await _characterService.Delete(id));
            }
    }
}