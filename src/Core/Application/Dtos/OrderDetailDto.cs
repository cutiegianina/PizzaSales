namespace Application.Dtos;
public class OrderDetailDto
{
    public int OrderDetailId { get; set; }
    public int OrderId { get; set; }
    public string PizzaId { get; set; } = string.Empty;
    public OrderDto? Order { get; set; }
    public PizzaDto? Pizza { get; set; }
    public int? Quantity { get; set; }
}