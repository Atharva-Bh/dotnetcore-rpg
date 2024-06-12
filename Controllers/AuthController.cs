using dotnetcore_rpg.Data;
using dotnetcore_rpg.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcore_rpg.Controllers
{
    [ApiController]
     [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _authRepo;
        public AuthController(IAuthRepo authRepo)
        {
            _authRepo = authRepo; 
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            var response = await _authRepo.Register(new User { Username = request.Username , 
            } , request.Password);
            if(response.Success is false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var response = await
            _authRepo.Login(request.Username , request.Password);
            if(!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}