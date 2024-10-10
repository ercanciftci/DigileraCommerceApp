namespace DigileraCommerceApp.Application.Wrappers.Commands.Orders.Request;

public class CreateOrderRequest
{
    public string ProductName { get; set; }

    public decimal Price { get; set; }
}