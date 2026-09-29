using ServiceOrderManager.Models;
using ServiceOrderManager.Models.ViewModels;

namespace ServiceOrderManager.Mappings
{
    public static class ClientMappingExtensions
    {
        // 1. Converte de ViewModel para Model (Usado no POST do Create/Edit)
        public static Client ToEntity(this ClientViewModel model)
        {
            if (model == null) return new Client();

            return new Client
            {
                Id = model.Id,
                Name = model.Name,
                EINumber = model.EINumber,
                PrimaryPhone = model.PrimaryPhone,
                PrimaryEmail = model.PrimaryEmail,

                // Mapeamento dos objetos aninhados (Assumindo que Address possui lógica similar)
                CompanyAddress = model.CompanyAddress != null ? new Address
                {
                    Id = model.CompanyAddress.Id != null ? model.CompanyAddress.Id : 0,
                    Street1 = model.CompanyAddress.Street1,
                    Street2 = model.CompanyAddress.Street2,
                    City = model.CompanyAddress.City,
                    State = model.CompanyAddress.State,
                    ZipCode = model.CompanyAddress.ZipCode,
                    Country = model.CompanyAddress.Country
                } : null,

                MailAddress = model.MailAddress != null ? new Address
                {
                    Id = model.MailAddress.Id != null ? model.MailAddress.Id : 0,
                    Street1 = model.MailAddress.Street1,
                    Street2 = model.MailAddress.Street2,
                    City = model.MailAddress.City,
                    State = model.MailAddress.State,
                    ZipCode = model.MailAddress.ZipCode,
                    Country = model.MailAddress.Country
                } : null
            };
        }

        // 2. Converte de Model para ViewModel (Usado no GET do Edit/Details)
        public static ClientViewModel ToViewModel(this Client entity)
        {
            if (entity == null) return new ClientViewModel();

            return new ClientViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                EINumber = entity.EINumber,
                PrimaryPhone = entity.PrimaryPhone,
                PrimaryEmail = entity.PrimaryEmail,

                CompanyAddress = entity.CompanyAddress != null ? new AddressViewModel
                {
                    // Mapeie de volta da entidade Address para o AddressViewModel
                    Id = entity.CompanyAddress.Id != null ? entity.CompanyAddress.Id : 0,
                    Street1 = entity.CompanyAddress.Street1,
                    Street2 = entity.CompanyAddress.Street2,
                    City = entity.CompanyAddress.City,
                    State = entity.CompanyAddress.State,
                    ZipCode = entity.CompanyAddress.ZipCode,
                    Country = entity.CompanyAddress.Country
                } : new AddressViewModel(),

                MailAddress = entity.MailAddress != null ? new AddressViewModel
                {
                    // Mapeie de volta da entidade Address para o AddressViewModel
                    Id = entity.MailAddress.Id != null ? entity.MailAddress.Id : 0,
                    Street1 = entity.MailAddress.Street1,
                    Street2 = entity.MailAddress.Street2,
                    City = entity.MailAddress.City,
                    State = entity.MailAddress.State,
                    ZipCode = entity.MailAddress.ZipCode,
                    Country = entity.MailAddress.Country
                } : new AddressViewModel()
            };
        }
    }
}
