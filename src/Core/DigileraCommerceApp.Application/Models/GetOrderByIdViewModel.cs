using System;
using System.Collections.Generic;
using System.Text;

namespace DigileraCommerceApp.Application.Models;

public class GetOrderByIdViewModel
{
    public int Id { get; set; }

    public DateTime CreateDate { get; set; }

    public string ProductName { get; set; }

    public decimal Price { get; set; }

    public int OrderStatus { get; set; }
}
