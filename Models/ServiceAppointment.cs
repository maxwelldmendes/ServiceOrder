

namespace ServiceOrderManager.Models
{
    public class ServiceAppointment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int ClientId { get; set; }
        public Client? ClientAssigned { get; set; }

        public int AssignedTechnicianId { get; set; }
        public Technician? AssignedTechnician { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

    }
}