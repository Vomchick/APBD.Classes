using Microsoft.EntityFrameworkCore;
using Tutorial10.Application.Repositories;
using Tutorial10.Core.DataModels;

namespace Tutorial10.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ClinicContext _context;

    public UserRepository(ClinicContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByUsernameAsync(string username, CancellationToken token)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username, token);
    }

    public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken token)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken, token);
    }

    public async Task AddUserAsync(User user, CancellationToken token)
    {
        await _context.Users.AddAsync(user, token);
    }

    public async Task SaveChangesAsync(CancellationToken token)
    {
        await _context.SaveChangesAsync(token);
    }
}
