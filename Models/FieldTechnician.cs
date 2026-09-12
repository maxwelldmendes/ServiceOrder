namespace ServiceOrderManager.Models
    public class FieldTechnician
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Skils { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool Active { get; set; } = true;

        // Relacionamento: 1 Técnico pode estar associado a várias OS
        public ICollection<ServiceOrder> DesignatedOrders { get; set; } = new List<ServiceOrder>();
    }
}