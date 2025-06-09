using Tutorial10.Core.DataModels;

namespace Tutorial10.Application.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAsync(string username, CancellationToken token);
    Task AddUserAsync(User user, CancellationToken token);
    Task SaveChangesAsync(CancellationToken token);
    Task<User?> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken token);
}
