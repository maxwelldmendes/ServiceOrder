namespace ServiceOrderManager.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EINumber { get; set; } = string.Empty;
        public string PrimaryPhone { get; set; } = string.Empty;
        public string PrimaryEmail { get; set; } = string.Empty;

        // Foreign Keys
        public int CompanyAddressId { get; set; }
        public int MailAddressId { get; set; }

        // Foreign Keys for strict One-to-One
        public Address? CompanyAddress { get; set; }
        public Address? MailAddress { get; set; }

        // Relacionamento: 1 Cliente possui muitas OS
        public ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
    }
}
