using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Proyecto_Progra_II.Models;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Proyecto_Progra_II.Services.Login;
using Proyecto_Progra_II.Services.Citas;
using Proyecto_Progra_II.Services.Usuarios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApiContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration["ConnectionString"]);
    
});

//Services

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ICitasService, CitasService>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();




var key = builder.Configuration.GetValue<string>("JwtSettings:key");
var keyBytes = Encoding.UTF8.GetBytes(key);

builder.Services.AddAuthentication("Bearer").AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
