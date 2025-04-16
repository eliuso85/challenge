using Domain.DTO;


namespace Infrastructure.Repository.Interfaces;

public interface IRepositoryGestion
{
    Task<User> saveUserAsync(User user);
}
