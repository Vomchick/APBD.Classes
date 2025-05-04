namespace Tutorial7.Services.Core;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
    public DateTime Now { get; }
}
