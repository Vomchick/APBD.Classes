namespace Test2.Exceptions;

public class PublishingHouseNotFoundException: Exception
{
    public PublishingHouseNotFoundException(int publishingHouseId)
        : base($"Publishing house with id {publishingHouseId} does not exist")
    {
    }
}
