using Domain.DTO;
using Domain.Request;
using Domain.Response;

namespace Infrastructure.Repository.Interfaces;

public interface IRepositoryLogin
{
    Task<User?> LoginUserAsync(LoginRequest user);
}
