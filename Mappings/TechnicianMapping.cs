using ServiceOrderManager.Models;
using ServiceOrderManager.Models.ViewModels;

namespace ServiceOrderManager.Mappings
{
    public static class TechnicianMapping
    {
        public static TechnicianViewModel ToViewModel(this Technician techs)
        {
            if (techs == null) return null!;

            return new TechnicianViewModel
            {
                Id = techs.Id,
                Skills = techs.Skills,

                Enabled = techs.Enabled,
            };
        }

        public static Technician ToModel(this TechnicianViewModel TechsviewModel)
        {
            if (TechsviewModel == null) return null!;

            return new Technician
            {
                Id = TechsviewModel.Id,
                Skills = TechsviewModel.Skills,
                Enabled = TechsviewModel.Enabled,
            };
        }
    }
}
