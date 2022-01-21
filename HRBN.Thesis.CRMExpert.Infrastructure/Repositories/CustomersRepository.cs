using System;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
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
        await Task.Factory.StartNew(() =>
        {
            _dbContext.Customers.Remove(entity);
        });
    }

    public Task<IPageResult<Customer>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Customer entity)
    {
        await _dbContext.Customers.AddAsync(entity);
    }

    public async Task UpdateAsync(Customer entity)
    {
        await Task.Factory.StartNew(() =>
        {
            _dbContext.Customers.Update(entity);
        });
    }
}