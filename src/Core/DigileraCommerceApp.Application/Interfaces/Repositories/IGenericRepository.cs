﻿using DigileraCommerceApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigileraCommerceApp.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync();

    Task<T?> GetByIdAsync(int id);

    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);
}
