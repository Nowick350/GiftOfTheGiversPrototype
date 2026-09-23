namespace GiftOfTheGiversPrototype.Models;

public class ReliefProject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Status { get; set; } = "Planning";
    public string Description { get; set; } = string.Empty;
}
