using Castle.Core.Configuration;
using DigileraCommerceApp.Application.Interfaces.Repositories;
using DigileraCommerceApp.Application.Models;
using DigileraCommerceApp.Application.Wrappers;
using DigileraCommerceApp.Application.Wrappers.Commands.Orders.Request;
using DigileraCommerceApp.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;

namespace DigileraCommerceApp.Test.IntegrationTests.Controllers;

public class OrdersControllerTests : BaseIntegrationTests, IClassFixture<ApiWebApplicationFactory>
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;

    private HttpClient _client;
    private string uri;

    public OrdersControllerTests(ApiWebApplicationFactory application)
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();

        _client = application.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddTransient<IOrderRepository>(f => _orderRepositoryMock.Object);
            });
        }).CreateClient();
        uri = $"https://localhost:7111/api/orders";
    }

    [Fact]
    public async Task GetOrderById_Handle_Should_ReturnSuccessResult_WhenValidParameter()
    {
        int id = 1;

        var expectedOrder = new Order()
        {
            Id = id,
            ProductName = "Test",
            OrderStatus = 0,
            Price = 100m
        };

        var url = $"{uri}/{id}";
        var request = CreateGetRequest(url);

        _orderRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(expectedOrder);

        var response = await _client.SendAsync(request);
        var result = await DeserializeResponse<ServiceResponse<GetOrderByIdViewModel>>(response);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(expectedOrder.Id);
    }

    [Fact]
    public async Task CreateOrder_Handle_Should_ReturnSuccessResult_WhenValidOrder()
    {
        var orderRequest = new CreateOrderRequest
        {
            ProductName = "Test",
            Price = 100m
        };

        var expectedOrder = new Order()
        {
            Id = 1,
            ProductName = "Test",
            Price = 100m,
            OrderStatus = 0
        };

        var url = $"{uri}";
        var request = CreatePostRequest(url, orderRequest);

        _orderRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedOrder);

        var response = await _client.SendAsync(request);
        var result = await DeserializeResponse<ServiceResponse<GetOrderByIdViewModel>>(response);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(expectedOrder.Id);
        _orderRepositoryMock.Verify(x => x.AddAsync(It.Is<Order>(m => m.Id == result.Value.Id), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CreateOrder_Handle_Should_ReturnSuccessResult_WhenInValidOrder()
    {
        var orderRequest = new CreateOrderRequest
        {
            Price = 100m
        };

        var url = $"{uri}";
        var request = CreatePostRequest(url, orderRequest);

        _orderRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()));

        var response = await _client.SendAsync(request);
        await AssertResponseStatusAsync(response, HttpStatusCode.BadRequest);
    }
}
