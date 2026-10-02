using ServiceOrderManager.Models;
using ServiceOrderManager.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace ServiceOrderManager.Mappings
{
    public static class TechnicianMapping
    {
        // Converte a Entidade do Banco para o FormViewModel (Edição)
        public static TechnicianViewModel ToViewModel(this Technician technician)
        {
            if (technician == null) return null!;

            return new TechnicianViewModel
            {
                //Tabela Technician
                Id = technician.Id,
                Skills = technician.Skills,
                Enabled = technician.Enabled,
                UserId = technician.UserId,
                //Tabela SystemUser
                FirstName = technician.User.FirstName,
                Middlename = technician.User.MiddleName,
                LastName = technician.User.LastName,
                UserName = technician.User.UserName,
                UserEmail = technician.User.Email,
                PhoneNumber = technician.User.PhoneNumber,
                UserRole = technician.User.UserRole
            };
        }

        // Cria uma nova Entidade a partir do Formulário (POST Create)
        public static Technician ToEntity(this TechnicianViewModel technicianViewModel)
        {
            if (technicianViewModel == null) return null!;

            return new Technician
            {
                Id = technicianViewModel.Id,
                Skills = technicianViewModel.Skills,
                Enabled = technicianViewModel.Enabled,
                UserId = technicianViewModel.UserId
            };
        }
    }
}
