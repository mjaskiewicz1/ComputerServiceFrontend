using Common.ViewModels;

namespace Common.Dtos;

public class RequestShipmentDetailDto
{
    public RequestShipmentDetailDto()
    {
    }

    public RequestShipmentDetailDto(RequestShipmentDetailViewModel requestShipmentDetailViewModel)
    {
        City = requestShipmentDetailViewModel.City;
        Street = requestShipmentDetailViewModel.Street;
        Postcode = requestShipmentDetailViewModel.Postcode.Replace("-", string.Empty);
    }

    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Postcode { get; set; } = null!;
}