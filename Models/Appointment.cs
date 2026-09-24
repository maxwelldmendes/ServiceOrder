using ServiceOrderManager.Models;

public class Appointment
{
    public int Id { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public string Title { get; set; } = string.Empty;

    public string AssignedUserId { get; set; } = string.Empty;
    public SystemUser AssignedUser { get; set; } = null!;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public AppointmentStatus Status { get; set; }

    public string? Description { get; set; }
}
