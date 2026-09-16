namespace ServiceOrderManager.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Street1 { get; set; } = null!;
        public string Street2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
