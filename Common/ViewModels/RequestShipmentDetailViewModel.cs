using Common.Dtos;

namespace Common.ViewModels;

public class RequestShipmentDetailViewModel : AddressViewModel
{
    public RequestShipmentDetailViewModel()
    {
    }

   

    public RequestShipmentDetailViewModel(RequestShipmentDetailDto model) : base(model.City, model.Street, model.Postcode)
    {
 
    }
}