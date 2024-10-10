using DigileraCommerceApp.Application.Interfaces.Repositories;
using DigileraCommerceApp.Domain.Entities;
using DigileraCommerceApp.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigileraCommerceApp.Persistence.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}
