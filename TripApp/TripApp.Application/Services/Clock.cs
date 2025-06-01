using TripApp.Application.Services.Interfaces;

namespace TripApp.Application.Services;

public class Clock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
