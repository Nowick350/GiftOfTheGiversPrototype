namespace GiftOfTheGiversPrototype.Models;

public class Donation
{
    public int Id { get; set; }
    public string DonorName { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "ZAR";
    public string Frequency { get; set; } = "One-Time";
    public string PaymentMethod { get; set; } = "Mock Card";
    public string ReceiptNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
