namespace ServiceOrderManager.Models
{ 
    public class Technician
    {
        public int Id { get; set; }
        public string Skils { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;

        // Relacionamento: 1 Técnico pode estar associado a várias OS
        public ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
    }
}