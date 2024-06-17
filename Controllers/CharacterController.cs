using dotnetcore_rpg.Dtos.Character;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace dotnetcore_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
            
        }
        //[AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
        //     var response = new 
        //     ServiceResponse<List<GetCharacterDto>>();
        //    // int id = int.Parse(User.Claims.FirstOrDefault
        //     //(u => u.Type == ClaimTypes.NameIdentifier ).Value ?? string.Empty);
        //     var claim = User.Claims.FirstOrDefault(
        //         u => u.Type == ClaimTypes.NameIdentifier);
        //         int id = int.Parse(claim?.Value ?? string.Empty);
        //         if (claim != null)
        //         {
        //              return Ok(await _characterService.GetAll(id));
        //         }
        //         else
        //         {
        //             response.Success = false;
        //             return BadRequest(response);
        //         }
        return Ok(await _characterService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var response = await
             _characterService.GetCharacterByID(id);
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