using AutoMapper;
using DigileraCommerceApp.Application.Interfaces.Repositories;
using DigileraCommerceApp.Application.Messages;
using DigileraCommerceApp.Application.Models;
using DigileraCommerceApp.Application.Wrappers;
using DigileraCommerceApp.Application.Wrappers.Commands.Orders.Request;
using DigileraCommerceApp.Domain.Entities;
using MassTransit;
using MediatR;

namespace DigileraCommerceApp.Application.Features.Commands.Orders;

public class CreateOrderCommand : IRequest<ServiceResponse<GetOrderByIdViewModel>>
{
    public required CreateOrderRequest Request { get; init; }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ServiceResponse<GetOrderByIdViewModel>>
    {
        IOrderRepository _orderRepository;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IOrderRepository orderRepository,
            ISendEndpointProvider sendEndpointProvider,
            IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _sendEndpointProvider = sendEndpointProvider;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetOrderByIdViewModel>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(command.Request);
            order.CreateDate = DateTime.UtcNow;
            order = await _orderRepository.AddAsync(order, cancellationToken);
            var dto = _mapper.Map<GetOrderByIdViewModel>(order);

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

            var createOrderMessageCommand = new CreateOrderMessageCommand
            {
                Id = order.Id
            };

            await sendEndpoint.Send(createOrderMessageCommand);

            return new ServiceResponse<GetOrderByIdViewModel>(dto);
        }
    }
}
