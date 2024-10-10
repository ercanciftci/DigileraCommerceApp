using DigileraCommerceApp.Application.Enums;
using DigileraCommerceApp.Application.Interfaces.Repositories;
using DigileraCommerceApp.Application.Messages;
using MassTransit;

namespace DigileraCommerceApp.Application.Consumers;

public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderMessageCommandConsumer(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
    {
        var order = await _orderRepository.GetByIdAsync(context.Message.Id);

        if (order == null || order.OrderStatus != (int)OrderStatuses.New)
        {
            return;
        }

        order.OrderStatus = (int)OrderStatuses.Confirmed;
        await _orderRepository.UpdateAsync(order);
    }
}
