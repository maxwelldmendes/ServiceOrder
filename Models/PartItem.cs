namespace ServiceOrderManager.Models
{
    public class PartItem
    {
        public int Id { get; set; }
        public string PartName { get; set; } = string.Empty;
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount => UnitPrice * Amount;

        public int ServiceOrderId { get; set; }
        public ServiceOrder ServiceOrder { get; set; } = null!;
    }

}