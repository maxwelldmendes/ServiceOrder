namespace ServiceOrderManager.Models
{
    public class ServiceItem
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal HourPrice { get; set; }
        public double WorkingHours { get; set; }
        public decimal TotalService => HourPrice * (decimal)WorkingHours;

        public int ServiceOrderId { get; set; }
        public ServiceOrder ServiceOrder { get; set; } = null!;
    }

}