using System.Security.Cryptography;
using System.Text;
using Tutorial10.Application.DTO;
using Tutorial10.Application.Repositories;
using Tutorial10.Application.Services.Interfaces;
using Tutorial10.Core.DataModels;

namespace Tutorial10.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;
    private readonly JwtHelper _jwtHelper;

    public AuthService(JwtHelper jwtHelper, IUserRepository repository)
    {
        _jwtHelper = jwtHelper;
        _repository = repository;
    }

    public async Task RegisterUserAsync(UserDto request, CancellationToken token)
    {
        if (await _repository.GetUserByUsernameAsync(request.Username, token) != null)
            throw new Exception("User already exists");

        CreatePasswordHash(request.Password, out byte[] hash, out byte[] salt);

        var user = new User
        {
            Username = request.Username,
            PasswordHash = hash,
            PasswordSalt = salt
        };

        await _repository.AddUserAsync(user, token);
        await _repository.SaveChangesAsync(token);
    }

    public async Task<AuthResponseDto> LoginAsync(UserDto request, CancellationToken token)
    {
        var user = await _repository.GetUserByUsernameAsync(request.Username, token) 
            ?? throw new Exception("User not found");

        if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            throw new Exception("Invalid password");

        var accessToken = _jwtHelper.GenerateAccessToken(user.Username);
        var refreshToken = _jwtHelper.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

        await _repository.SaveChangesAsync(token);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<string> RefreshTokenAsync(string refreshToken, CancellationToken token)
    {
        var user = await _repository.GetUserByRefreshTokenAsync(refreshToken, token)
                   ?? throw new Exception("Invalid refresh token");

        if (user.RefreshTokenExpiryTime < DateTime.Now)
            throw new Exception("Refresh token expired");

        var newAccessToken = _jwtHelper.GenerateAccessToken(user.Username);
        return newAccessToken;
    }

    private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
    {
        using var hmac = new HMACSHA512(salt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return hash.SequenceEqual(computedHash);
    }
}

