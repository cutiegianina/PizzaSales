namespace Application.Dtos;
public class PizzaDto
{
    public string PizzaId { get; set; } = string.Empty;
    public string? PizzaTypeId { get; set; }
    public PizzaTypeDto? PizzaType { get; set; }
    public string? Size { get; set; }
    public decimal? Price { get; set; }
}