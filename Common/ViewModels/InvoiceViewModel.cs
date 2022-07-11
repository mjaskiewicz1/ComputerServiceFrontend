using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace Common.ViewModels;

public class InvoiceViewModel
{
    public string Rma { get; set; } = null!;

    [Display(Name = "Numer Faktury")] public string Number { get; set; } = null!;

    [Display(Name = "Data wystawienia")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Data wystawienia wymagana")]
    public DateTime InvoiceDate { get; set; } = DateTime.Now;

    [Display(Name = "Suma")] public decimal Total { get; set; }

    [Display(Name = "Status płatności")] public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Opłacone;
}