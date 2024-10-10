using DigileraCommerceApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigileraCommerceApp.Domain.Entities;

public class Order : BaseEntity
{
    public string ProductName { get; set; }

    public decimal Price { get; set; }

    public int OrderStatus { get; set; }
}
