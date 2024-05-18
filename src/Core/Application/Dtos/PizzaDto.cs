﻿using Domain.Entities;

namespace Application.Dtos;
public class PizzaDto
{
    public string PizzaId { get; set; }
    public string? PizzaTypeId { get; set; }
    public PizzaType? PizzaType { get; set; }
    public string? Size { get; set; }
    public decimal? Price { get; set; }
}