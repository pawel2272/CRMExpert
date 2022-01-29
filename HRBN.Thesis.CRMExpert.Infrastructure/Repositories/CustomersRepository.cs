using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;
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
        string lowerCaseSearchPhrase = searchPhrase?.ToLower();

        var baseQuery = _dbContext.Customers
            .Where(c => (searchPhrase == null ||
                         c.Id.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                         || c.Name.ToLower().Contains(lowerCaseSearchPhrase)
                         || c.City.ToLower().Contains(lowerCaseSearchPhrase)
                         || c.Street.ToLower().Contains(lowerCaseSearchPhrase)
                         || c.PostalCode.ToLower().Contains(lowerCaseSearchPhrase)
                         || c.TaxNo.ToLower().Contains(lowerCaseSearchPhrase)
                         || c.Regon.ToLower().Contains(lowerCaseSearchPhrase))
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
                {nameof(Customer.Regon), c => c.Regon},
                {nameof(Customer.CreDate), c => c.CreDate},
                {nameof(Customer.ModDate), c => c.ModDate}
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

        return new PageResult<Customer>(entities, baseQuery.Count(), pageSize, pageNumber, searchPhrase, sortDirection, orderBy);
    }

    public async Task AddAsync(Customer entity)
    {
        await _dbContext.Customers.AddAsync(entity);
    }

    public async Task UpdateAsync(Customer entity)
    {
        await Task.Factory.StartNew(() => { _dbContext.Customers.Update(entity); });
    }

    public async Task<List<CustomerDataDto>> GetCustomerDataAsync()
    {
        var results = await _dbContext
            .Customers
            .Select(e => new CustomerDataDto() {Id = e.Id, Name = e.Name})
            .ToListAsync();
        return results;
    }
}