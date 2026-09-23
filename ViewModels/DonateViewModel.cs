using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversPrototype.ViewModels;

public class DonateViewModel
{
    [Required]
    [Range(1, 10000000)]
    [Display(Name = "Donation Amount")]
    public decimal Amount { get; set; }

    [Required]
    public string Currency { get; set; } = "ZAR";

    [Required]
    public string Frequency { get; set; } = "One-Time";

    [Required]
    [Display(Name = "Donor Name")]
    public string DonorName { get; set; } = string.Empty;

    [Display(Name = "Payment Method")]
    public string PaymentMethod { get; set; } = "Mock Card";
}
