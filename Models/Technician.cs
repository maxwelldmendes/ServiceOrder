namespace ServiceOrderManager.Models;

public class Technician
{
    public int Id { get; set; }
    public string Skills { get; set; } = string.Empty;
    public bool Enabled { get; set; }
    public string UserId { get; set; } = string.Empty ;
    public virtual SystemUser? User { get; set; } = null;
    public virtual ICollection<ServiceAppointment> ServiceAppointments { get; set; } = new List<ServiceAppointment>();
    public virtual ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
   
}