using Microsoft.EntityFrameworkCore;
using Proyecto_Progra_II.Models;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Proyecto_Progra_II.Services.Login;
using Proyecto_Progra_II.Services.Citas;
using Proyecto_Progra_II.Services.Usuarios;
using Proyecto_Progra_II.Services.Especialidades;
using Services.Services.EstadosCitas;
using Services.Services.Roles;
using Services.Services.Sucursales;
using Models.Models.Custom;
using Services.Services.Email;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("*")
            .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("sql"));
});

// Services
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ICitasService, CitasService>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IEspecialidadService, EspecialidadService>();
builder.Services.AddScoped<IEstadoCitaService, EstadoCitaService>();
builder.Services.AddScoped<IRolesService, RolesService>();
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

app.UseCors("AllowAll");

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
