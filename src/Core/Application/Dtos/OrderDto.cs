using Domain.Entities;

namespace Application.Dtos;
public class OrderDto : OrderDateTime
{
    public int OrderId { get; set; }
}