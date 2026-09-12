using System;
using System.Collections.Generic;

namespace ServiceOrderManager.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IENumber { get; set; } = string.Empty;
        public string PrimariPhone { get; set; } = string.Empty;
        public string PrimeryEmail { get; set; } = string.Empty;
        public Address CompanyEndereco { get; set; } = new();
        public Address MailAddress { get; set; } = new();

        // Relacionamento: 1 Cliente possui muitas OS
        public ICollection<ServiceOrder> ServiceOrder { get; set; } = new List<ServiceOrder>();
    }
