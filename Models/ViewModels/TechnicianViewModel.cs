namespace ServiceOrderManager.Models.ViewModels
{
    public class TechnicianViewModel
    {
        public int Id { get; set; }

        // FK para AspNetUsers.Id
        public string UserId { get; set; } = string.Empty;

        // Navegação para o usuário
        public SystemUser User { get; set; } = null!;

        public string Skills { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;

        // Se necessário, use o PhoneNumber herdado de IdentityUser
        // Não é necessário declarar outro PhoneNumber aqui.

        public ICollection<ServiceOrder> ServiceOrders { get; set; }
            = new List<ServiceOrder>();
    }
}