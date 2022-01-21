using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;
using Microsoft.EntityFrameworkCore;

namespace HRBN.Thesis.CRMExpert.Infrastructure.Repositories;

public class CustomersRepository : ICustomersRepository
{
    private readonly CRMContext _dbContext;

    public CustomersRepository(CRMContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer> GetAsync(Guid id)
    {
        return await _dbContext.Customers.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    public async Task DeleteAsync(Customer entity)
    {
        await Task.Factory.StartNew(() => { _dbContext.Customers.Remove(entity); });
    }

    public async Task<IPageResult<Customer>> SearchAsync(string searchPhrase, int pageNumber, int pageSize,
        string orderBy,
        SortDirection sortDirection)
    {
        var baseQuery = _dbContext.Customers
            .Where(c => (searchPhrase == null ||
                         c.Id.ToString().Contains(searchPhrase)
                         || c.Name.ToLower().Contains(searchPhrase.ToLower())
                         || c.City.ToLower().Contains(searchPhrase.ToLower())
                         || c.Street.ToLower().Contains(searchPhrase.ToLower())
                         || c.PostalCode.ToLower().Contains(searchPhrase.ToLower())
                         || c.TaxNo.ToLower().Contains(searchPhrase.ToLower())
                         || c.Regon.ToLower().Contains(searchPhrase.ToLower()))
            );
        if (!string.IsNullOrEmpty(orderBy))
        {
            var columnSelectors = new Dictionary<string, Expression<Func<Customer, object>>>()
            {
                {nameof(Customer.Name), c => c.Name},
                {nameof(Customer.City), c => c.City},
                {nameof(Customer.Street), c => c.Street},
                {nameof(Customer.PostalCode), c => c.PostalCode},
                {nameof(Customer.TaxNo), c => c.TaxNo},
                {nameof(Customer.Regon), c => c.Regon}
            };

            Expression<Func<Customer, object>> selectedColumn;

            if (columnSelectors.Keys.Contains(orderBy))
            {
                selectedColumn = columnSelectors[orderBy];
            }
            else
            {
                selectedColumn = columnSelectors["Name"];
            }

            baseQuery = sortDirection == SortDirection.ASC
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var entities = await baseQuery.Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return new PageResult<Customer>(entities, baseQuery.Count(), pageSize, pageNumber);
    }

    public async Task AddAsync(Customer entity)
    {
        await _dbContext.Customers.AddAsync(entity);
    }

    public async Task UpdateAsync(Customer entity)
    {
        await Task.Factory.StartNew(() => { _dbContext.Customers.Update(entity); });
    }
}