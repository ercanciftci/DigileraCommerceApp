using DigileraCommerceApp.Application.Dtos;
using DigileraCommerceApp.Application.Features.Commands.Orders;
using DigileraCommerceApp.Application.Features.Queries.GetAllOrders;
using DigileraCommerceApp.Application.Features.Queries.GetOrderById;
using DigileraCommerceApp.Application.Models;
using DigileraCommerceApp.Application.Wrappers;
using DigileraCommerceApp.Application.Wrappers.Commands.Orders.Request;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DigileraCommerceApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly MediatR.IMediator _mediator;

    public OrdersController(MediatR.IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}", Name = "orders-get-by-id")]
    [ProducesResponseType(typeof(ServiceResponse<GetOrderByIdViewModel>), (int)HttpStatusCode.OK)]
    public async Task<ServiceResponse<GetOrderByIdViewModel>> GetByIdAsync(int id)
    {
        var query = new GetOrderByIdQuery() 
        { 
            Id = id 
        };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet(Name = "orders-get-list")]
    [ProducesResponseType(typeof(ServiceResponse<List<OrderViewDto>>), (int)HttpStatusCode.OK)]
    public async Task<ServiceResponse<List<OrderViewDto>>> GetAllAsync()
    {
        var query = new GetAllOrdersQuery();
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpPost(Name = "orders-create")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ServiceResponse<GetOrderByIdViewModel>> Post([FromBody] CreateOrderRequest request)
    {
        var command = new CreateOrderCommand()
        {
            Request = request
        };
        var result = await _mediator.Send(command);
        return result;
    }
}
