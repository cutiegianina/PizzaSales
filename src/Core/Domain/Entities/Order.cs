namespace Domain.Entities;
public class Order : OrderDateTime
{
    public Guid OrderId { get; set; }
}