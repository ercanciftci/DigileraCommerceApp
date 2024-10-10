using AutoMapper;
using DigileraCommerceApp.Application.Dtos;
using DigileraCommerceApp.Application.Interfaces.Repositories;
using DigileraCommerceApp.Application.Wrappers;
using MediatR;

namespace DigileraCommerceApp.Application.Features.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<ServiceResponse<List<OrderViewDto>>>
    {
        public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, ServiceResponse<List<OrderViewDto>>>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IMapper _mapper;

            public GetAllOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<List<OrderViewDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
            {
                var orders = await _orderRepository.GetAllAsync();
                var viewModel = _mapper.Map<List<OrderViewDto>>(orders);
                return new ServiceResponse<List<OrderViewDto>>(viewModel);
            }
        }
    }
}
