namespace Common.Helppers;

public class FileHelper
{
    public FileHelper(string contentType, Stream fileStream, string name)
    {
        ContentType = contentType;
        FileStream = fileStream;
        Name = name;
    }

    public FileHelper()
    {
    }

    public string Name { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public Stream FileStream { get; set; } = null!;
}