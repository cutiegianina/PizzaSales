namespace Domain.Entities;
public class Order : OrderDateTime
{
    public int OrderId { get; set; }
    public Guid? CustomerId { get; set; }
    public int? PaymentMethodId { get; set; }
    public Customer? Customer { get; set; }
    public decimal? TotalAmount { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public string? Status { get; set; }
}