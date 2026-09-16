using ServiceOrderManager.Models;
using ServiceOrderManager.Models.ViewModels;

namespace ServiceOrderManager.Mappings
{
    public static class ClientMapping
    {
        public static ClientViewModel ToViewModel(this Client client)
        {
            if (client == null) return null!;

            return new ClientViewModel
            {
                Id = client.Id,
                Name = client.Name,
                EINumber = client.EINumber,
                PrimaryPhone = client.PrimaryPhone,
                PrimaryEmail = client.PrimaryEmail,
                CompanyAddressId = client.CompanyAddressId,
                MailAddressId = client.MailAddressId,
                CompanyAddress = client.CompanyAddress?.ToViewModel(),
                MailAddress = client.MailAddress?.ToViewModel()
            };
        }

        public static Client ToModel(this ClientViewModel viewModel)
        {
            if (viewModel == null) return null!;

            return new Client
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                EINumber = viewModel.EINumber,
                PrimaryPhone = viewModel.PrimaryPhone,
                PrimaryEmail = viewModel.PrimaryEmail,
                CompanyAddressId = viewModel.CompanyAddressId,
                MailAddressId = viewModel.MailAddressId,
                CompanyAddress = viewModel.CompanyAddress?.ToModel(),
                MailAddress = viewModel.MailAddress?.ToModel()
            };
        }
    }
}
