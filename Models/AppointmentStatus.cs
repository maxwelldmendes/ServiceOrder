using System.ComponentModel.DataAnnotations;

public enum AppointmentStatus
{
    [Display(Name = "Agendado")]
    Scheduled,
    [Display(Name = "Confirmado")]
    Confirmed,
    [Display(Name = "Concluído")]
    Completed,
    [Display(Name = "Cancelado")]
    Cancelled
}