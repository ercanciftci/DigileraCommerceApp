using DigileraCommerceApp.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigileraCommerceApp.Application.Dtos;

public class OrderViewDto
{
    public int Id { get; set; }

    public string ProductName { get; set; }

    public decimal Price { get; set; }

    public OrderStatuses OrderStatus { get; set; }
}
