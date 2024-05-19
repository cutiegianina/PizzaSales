namespace Application.Dtos;

public class PizzaTypeDto
{
    public string PizzaTypeId { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? Ingredients { get; set; }
}