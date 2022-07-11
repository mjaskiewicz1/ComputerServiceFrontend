using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace Common.ViewModels;

public class RequestFilterViewModel
{
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Display(Name = "Status")] public List<RequestStatus>? RequestStatus { get; set; }

    [Display(Name = "Od")]
    [DataType(DataType.Date)]
    public DateTime? CreateDateTimeFrom { get; set; }

    [Display(Name = "Do")]
    [DataType(DataType.Date)]
    public DateTime? CreateDateTimeTo { get; set; }

    public RequestFilterViewModel()
    {
        
    }
}