namespace Domain.Entities;
public class PizzaType
{
    public string PizzaTypeId { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? Ingredients { get; set; }
}