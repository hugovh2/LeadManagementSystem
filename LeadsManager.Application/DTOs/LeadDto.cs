namespace LeadsManager.Application.DTOs;

public class LeadDto
{
    public Guid Id { get; set; }
    public string ContactFirstName { get; set; } = string.Empty;
    public string ContactLastName { get; set; } = string.Empty;
    public string ContactFullName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string ContactPhoneNumber { get; set; } = string.Empty;
    public string Suburb { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? AcceptedAt { get; set; }
    public DateTime? DeclinedAt { get; set; }
}
