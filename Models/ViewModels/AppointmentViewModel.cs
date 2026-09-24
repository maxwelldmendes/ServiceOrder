using System.ComponentModel.DataAnnotations;

public class AppointmentViewModel
{
    public int Id { get; set; }

    [Required]
    public int ClientId { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string AssignedUserId { get; set; } = string.Empty;

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    public string? Description { get; set; }
}
