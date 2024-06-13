using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace dotnetcore_rpg.Data
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepo(DataContext context , IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            try{
            User user = await _context.Users.
            FirstAsync(u => u.Username.ToLower() == username.ToLower());
            if(!VerifyPassword(password , user.PasswordHash , 
            user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "User not found!";
                return response;
            }
            else
            {
                response.Data = CreateToken(user);
            }
            }catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response = new ServiceResponse<int>();
            if(await UserExists(user.Username))
            {
                response.Message = $" The user {user.Username} already exists!";
                response.Success = false;
                return response;
            }
            CreatePasswordHash
            (password , out byte[] passwordHash ,
             out byte[] passwordSalt);
             user.PasswordHash = passwordHash;
             user.PasswordSalt = passwordSalt;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            //var response = new ServiceResponse<int>();
            response.Data = user.ID;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if( 
                (await _context.Users.
                FirstOrDefaultAsync(u => u.Username == username)) 
                is null)
            {
                return false;
            }
            return true;
        }

        private void CreatePasswordHash
        (string password , out byte[] passwordHash , out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.
                ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPassword(string password , byte[] passwordHash , byte[] passwordSalt)
        {
             using(var hmac = new System.Security.Cryptography.
             HMACSHA512(passwordSalt))
            {
                var passwordHash2 = hmac.
                ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i< passwordHash2.Length; i++)
                {
                    if(passwordHash2[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier , user.ID.ToString()) ,
                new Claim(ClaimTypes.Name , user.Username) 
            };
                SymmetricSecurityKey key = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration.GetSection
                ("AppSetting:Token").Value ?? string.Empty));
                SigningCredentials creds = new SigningCredentials
            (key , SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims) , 
                Expires = DateTime.Now.AddDays(1) , 
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}