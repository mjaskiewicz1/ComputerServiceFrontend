namespace Common.Exceptions;

public class UniqueDuplicateException : Exception
{
    public UniqueDuplicateException(string key, string description)
    {
        Key = key;
        Description = description;
    }

    public string Key { get; } = null!;
    public string Description { get; } = null!;
}