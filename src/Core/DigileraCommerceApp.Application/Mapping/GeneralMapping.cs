using AutoMapper;
using DigileraCommerceApp.Application.Dtos;
using DigileraCommerceApp.Application.Features.Commands.Orders;
using DigileraCommerceApp.Application.Models;
using DigileraCommerceApp.Application.Wrappers.Commands.Orders.Request;
using DigileraCommerceApp.Domain.Entities;

namespace DigileraCommerceApp.Application.Mapping;

public class GeneralMapping: Profile
{
    public GeneralMapping()
    {
        CreateMap<Order, OrderViewDto>()
            .ReverseMap();

        CreateMap<Order, CreateOrderRequest>()
            .ReverseMap();

        CreateMap<Order, GetOrderByIdViewModel>()
            .ReverseMap();
    }
}
