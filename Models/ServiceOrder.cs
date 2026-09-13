using ServiceOrderManager.Models;

public class ServiceOrder
{
    public int Id { get; set; }
    public string Protocol { get; set; } = string.Empty; // Ex: OS-2026-0001
    public DateTime OpenDate { get; set; } = DateTime.UtcNow;
    public DateTime? ClosedDate { get; set; }
    public StatusOS Status { get; set; } = StatusOS.Open;
    public string ProblemDescription { get; set; } = string.Empty;
    public string TechnicalSolution { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }

    // Relacionamento Cliente (Obrigatório)
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    // Relacionamento Técnico (Opcional na abertura, obrigatório ao iniciar)
    public int? TechnicianId { get; set; }
    public Technician? Technician { get; set; }

    // Composições da OS
    public ICollection<ServiceItem> ServiceItem { get; set; } = new List<ServiceItem>();
    public ICollection<PartItem> PartItems { get; set; } = new List<PartItem>();
}

