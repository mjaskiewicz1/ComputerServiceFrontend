namespace Common.Dtos;

public class RequestHistoryDto
{
    public DateTime PeriodStart { get; set; }
    public string RequestStatus { get; set; } = null!;
    public string? EmployeeObjectId { get; set; }
}