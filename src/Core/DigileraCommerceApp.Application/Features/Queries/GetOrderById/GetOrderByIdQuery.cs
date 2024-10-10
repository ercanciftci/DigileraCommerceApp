using AutoMapper;
using DigileraCommerceApp.Application.Interfaces.Repositories;
using DigileraCommerceApp.Application.Models;
using DigileraCommerceApp.Application.Wrappers;
using MediatR;

namespace DigileraCommerceApp.Application.Features.Queries.GetOrderById;

public class GetOrderByIdQuery: IRequest<ServiceResponse<GetOrderByIdViewModel>>
{
    public int Id { get; set; }

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ServiceResponse<GetOrderByIdViewModel>>
    {
        IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetOrderByIdViewModel>> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(query.Id);
            var dto = _mapper.Map<GetOrderByIdViewModel>(order);

            return new ServiceResponse<GetOrderByIdViewModel>(dto);
        }
    }
}
