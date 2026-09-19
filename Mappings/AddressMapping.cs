using ServiceOrderManager.Models;
using ServiceOrderManager.Models.ViewModels;

namespace ServiceOrderManager.Mappings
{
    public static class AddressMapping
    {
        public static AddressViewModel ToViewModel(this Address address)
        {
            if (address == null) return null!;
            return new AddressViewModel
            {
                Id = address.Id,
                Street1 = address.Street1,
                Street2 = address.Street2 ?? string.Empty,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode,
                Country = address.Country
            };
        }

        public static Address ToModel(this AddressViewModel viewModel)
        {
            if (viewModel == null) return null!;
            return new Address
            {
                Id = viewModel.Id,
                Street1 = viewModel.Street1,
                Street2 = viewModel.Street2 ?? string.Empty,
                City = viewModel.City,
                State = viewModel.State,
                ZipCode = viewModel.ZipCode,
                Country = viewModel.Country
            };
        }

    }
}
