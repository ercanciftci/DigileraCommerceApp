using DigileraCommerceApp.Application.Interfaces.Repositories;
using DigileraCommerceApp.Persistence.Context;
using DigileraCommerceApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigileraCommerceApp.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceRegistration(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("memoryDb"));

        services.AddTransient<IOrderRepository, OrderRepository>();
    }
}