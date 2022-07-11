namespace Common.Helppers;

public class RequestAfSettings
{
    public string Url { get; set; } = null!;
    public string? GetAll { get; set; }
    public string? GetByRmaUrl { get; set; }
    public string? Create { get; set; }
    public string? Delete { get; set; }
    public string? GetByRma { get; set; }
    public string? Update { get; set; }
    public string? GetHistoryAll { get; set; }
    public string? Filter { get; set; }
}