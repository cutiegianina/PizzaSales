namespace Domain.Entities;
public class OrderDetail
{
    public int OrderDetailId { get; set; }
    public int OrderId { get; set; }
    public string PizzaId { get; set; } = string.Empty;
    public Order? Order { get; set; }
    public Pizza? Pizza { get; set; }
    public int? Quantity { get; set; }
}