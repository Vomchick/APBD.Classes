namespace Test2.Exceptions;

public class AuthorNotFoundException : Exception
{
    public AuthorNotFoundException(int authorId)
        : base($"Author with id {authorId} does not exist")
    {
    }
}
