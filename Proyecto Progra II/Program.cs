
using Microsoft.EntityFrameworkCore;
using Proyecto_Progra_II.Models;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Proyecto_Progra_II.Services.Login;
using Proyecto_Progra_II.Services.Citas;
using Proyecto_Progra_II.Services.Usuarios;
using Proyecto_Progra_II.Services.Especialidades;
using Proyecto_Progra_II.Services.EstadosCitas;
using Services.Services.EstadosCitas;
using Proyecto_Progra_II.Services.Roles;
using Services.Services.Roles;
using Proyecto_Progra_II.Services.Sucursales;
using Services.Services.Sucursales;

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
builder.Services.AddScoped<IEspecialidadService,EspecialidadService>();
builder.Services.AddScoped<IEstadoCitaService, EstadoCitaService>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<IEstadoCitaService, EstadoCitaService>();
builder.Services.AddScoped<ISucursalService, SucursalService>();


builder.Services.AddAuthentication("Bearer").AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
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
