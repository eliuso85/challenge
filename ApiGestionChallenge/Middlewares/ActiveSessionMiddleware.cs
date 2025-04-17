using Domain.DTO;
using Infrastructure.Repository.Data;
using Infrastructure.Repository.Interfaces;
using System.Data;
using System.Security.Claims;

namespace ApiGestionChallenge.Middlewares;
public class ActiveSessionMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
{
    private readonly RequestDelegate _next = next;
    private readonly IServiceScopeFactory _scopeFactory= scopeFactory;
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            using var scope = _scopeFactory.CreateScope();
            var repositoryLogin = scope.ServiceProvider.GetRequiredService<IRepositoryLogin>();

            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var token = context.Request.Headers.Authorization.FirstOrDefault()?.Replace("Bearer ", "");

            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token o usuario inválido.");
                return;
            }

            var  user = new User
            {
                Id = int.Parse(userId),
            };

            var session = await repositoryLogin.LoginUserActiveAsync(user);

            if (session == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Sesión no activa o token inválido.");
                return;
            }
        }

        await _next(context); // continuar si la sesión está activa o no es requerida
    }
}

