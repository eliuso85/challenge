using ApiGestionChallenge.Middlewares;
using Application.Interfaces;
using Application.Services;
using Domain.DTO;
using Infrastructure.Authentication;
using Infrastructure.Repository.Data;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Se obtiene el Connection String de a BD.
var connectionString = builder.Configuration.GetConnectionString("DefaultConn");
builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));

// ========================================
// CONFIGURACIÓN
// ========================================
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings")
);

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<JwtSettings>>().Value);


//var redisConnectionString = builder.Configuration.GetConnectionString("Redis");

//if (string.IsNullOrWhiteSpace(redisConnectionString))
//    throw new InvalidOperationException("Redis connection string is not configured.");

//builder.Services.AddSingleton<IConnectionMultiplexer>(
//    ConnectionMultiplexer.Connect(redisConnectionString)
//);

// ========================================
// INYECCIÓN DE DEPENDENCIAS
// ========================================
//Se registra el Generador del Token
builder.Services.AddScoped<JwtTokenGenerator>();
// Se agrega el Servicio y la interfaz para la Gestión y Control de Sesiones en el Recurso Compartido 
builder.Services.AddScoped<IRepositoryGestion, RepositoryGestion>();
builder.Services.AddScoped<IGestionService, GestionService>();
// se agrega el Servicio y la Interfaz del Login 
builder.Services.AddScoped<IRepositoryLogin, RepositoryLogin>();
builder.Services.AddScoped<ILoginService, LoginService>();
// se agrega el Servicio y la Interfaz de la Session 
builder.Services.AddScoped<IRepositorySession, RepositorySession>();
builder.Services.AddScoped<ISessionService, SessionService>();
// ========================================
// AUTENTICACIÓN JWT
// ========================================
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings!.Key!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

// ========================================
// CORS
// ========================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// ========================================
// CONTROLADORES + SWAGGER
// ========================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ========================================
// MIDDLEWARE
// ========================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseMiddleware<ActiveSessionMiddleware>(); // 2. Valida sesión activa en BD
app.UseAuthorization();
app.MapControllers();
app.Run();

