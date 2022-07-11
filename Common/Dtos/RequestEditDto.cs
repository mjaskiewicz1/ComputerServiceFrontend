using Common.Enums;

namespace Common.Dtos;

public class RequestEditDto
{
    public string Rma { get; set; }
    public Guid Url { get; set; }
    public RequestStatus RequestStatus { get; set; }
}