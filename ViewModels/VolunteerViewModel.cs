using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversPrototype.ViewModels;

public class VolunteerViewModel
{
    [Required]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Skills { get; set; } = string.Empty;

    [Required]
    public string Availability { get; set; } = string.Empty;
}
