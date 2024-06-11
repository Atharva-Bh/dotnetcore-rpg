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
            var response = await _characterService.GetCharacterByID(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
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
            var result = 
            await _characterService.UpdateCharacter(updatedCharacter);
            if(result.Data is null)
            {
                return NotFound(result);
            }
                return Ok(result);
        }
        [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var result = 
                await _characterService.Delete(id);
                if(result.Success is false)
                {
                    return BadRequest(result);
                }
                    return Ok(result);
            }
    }
}