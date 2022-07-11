using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Common.ViewModels;

public class RequestHistoryViewModel
{
    [JsonConstructor]
    public RequestHistoryViewModel(DateTime periodStart, string requestStatus)
    {
        PeriodStart = periodStart.ToLocalTime();
        RequestStatus = requestStatus;
    }

    [Display(Name = "Data")]
    [DataType(DataType.DateTime)]
    public DateTime PeriodStart { get; set; }

    [Display(Name = "Status")] public string RequestStatus { get; set; } = null!;
}