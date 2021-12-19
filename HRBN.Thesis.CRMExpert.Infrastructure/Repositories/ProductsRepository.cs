using System;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Infrastructure.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly CRMContext _dbContext;

    public ProductsRepository(CRMContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<Product> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task<IPageResult<Product>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Product entity)
    {
        throw new NotImplementedException();
    }
}