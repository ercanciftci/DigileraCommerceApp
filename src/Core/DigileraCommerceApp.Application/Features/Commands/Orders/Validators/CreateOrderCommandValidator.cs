using FluentValidation;

namespace DigileraCommerceApp.Application.Features.Commands.Orders.Validators;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(p => p.Request.ProductName)
            .NotNull()
            .WithMessage("Product name is required!");

        RuleFor(p => p.Request.Price)
            .NotNull()
            .WithMessage("Price is required!");
    }
}
