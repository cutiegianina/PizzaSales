using Domain.Entities;

namespace Application.Dtos;
public class OrderDto : OrderDateTime
{
    public int OrderId { get; set; }
    public Guid? CustomerId { get; set; }
    public int? PaymentMethodId { get; set; }
    public CustomerDto? Customer { get; set; }
    public decimal? TotalAmount { get; set; }
    public PaymentMethodDto? PaymentMethod { get; set; }
    public string? Status { get; set; }
}