global using dotnetcore_rpg.Services.CharacterService;
global using dotnetcore_rpg.Models;
using dotnetcore_rpg.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>
(x => x.
UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<ICharacterService , CharacterService>();
builder.Services.AddScoped<IAuthRepo , AuthRepo>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
AddJwtBearer(options => 
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true , 
        IssuerSigningKey= new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(builder.Configuration.GetSection
                ("AppSetting:Token").Value ?? string.Empty)) , 
        ValidateIssuer = false , 
        ValidateAudience = false
    };
});
builder.Services.AddSingleton<IHttpContextAccessor , HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
