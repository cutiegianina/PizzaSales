namespace Domain.Entities;
public class Pizza
{
    public string PizzaId { get; set; } = string.Empty;
    public string? PizzaTypeId { get; set; }
    public PizzaType? PizzaType { get; set; }
    public string? Size { get; set; }
    public decimal? Price { get; set; }
}