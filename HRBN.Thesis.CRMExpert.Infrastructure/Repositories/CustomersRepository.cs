using System;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Infrastructure.Repositories;

public class CustomersRepository : ICustomersRepository
{
    private readonly CRMContext _dbContext;

    public CustomersRepository(CRMContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<Customer> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Customer entity)
    {
        throw new NotImplementedException();
    }

    public Task<IPageResult<Customer>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Customer entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Customer entity)
    {
        throw new NotImplementedException();
    }
}