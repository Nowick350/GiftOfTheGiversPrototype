using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversPrototype.ViewModels;

public class ProjectViewModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Location { get; set; } = string.Empty;

    [Required]
    public string Status { get; set; } = "Planning";

    [Required]
    public string Description { get; set; } = string.Empty;
}
