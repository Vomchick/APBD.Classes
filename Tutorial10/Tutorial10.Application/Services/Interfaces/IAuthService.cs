using Tutorial10.Application.DTO;

namespace Tutorial10.Application.Services.Interfaces;

public interface IAuthService
{
    Task RegisterUserAsync(UserDto request, CancellationToken token);
    Task<AuthResponseDto> LoginAsync(UserDto request, CancellationToken token);
    Task<string> RefreshTokenAsync(string refreshToken, CancellationToken token);
}
