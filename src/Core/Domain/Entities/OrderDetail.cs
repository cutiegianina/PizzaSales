namespace Domain.Entities;
public class OrderDetail
{
    public Guid OrderDetailId { get; set; }
    public Guid OrderId { get; set; }
    public string PizzaId { get; set; }
    public Order? Order { get; set; }
    public Pizza? Pizza { get; set; }
    public int? Quantity { get; set; }
}