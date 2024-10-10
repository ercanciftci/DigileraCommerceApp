using DigileraCommerceApp.Application;
using DigileraCommerceApp.Application.Consumers;
using DigileraCommerceApp.Persistence;
using MassTransit;

namespace DigileraCommerceApp.WebApi;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        builder.Services.AddMassTransit(x =>
        {
            x.AddConsumer<CreateOrderMessageCommandConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMQ:HostName"], Convert.ToUInt16(configuration["RabbitMQ:Port"]), "/", host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

                cfg.ReceiveEndpoint("create-order-service", e =>
                {
                    e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
                });
            });
        });

        builder.Services.AddApplicationRegistration();
        builder.Services.AddPersistenceRegistration();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty;
        });

        app.MapControllers();

        app.Run();
    }
}
