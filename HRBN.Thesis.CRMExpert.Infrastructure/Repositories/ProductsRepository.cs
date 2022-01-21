using System;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HRBN.Thesis.CRMExpert.Infrastructure.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly CRMContext _dbContext;

    public ProductsRepository(CRMContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product> GetAsync(Guid id)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task DeleteAsync(Product entity)
    {
        await Task.Factory.StartNew(() => { _dbContext.Products.Remove(entity); });
    }

    public async Task<IPageResult<Product>> SearchAsync(string searchPhrase, int pageNumber, int pageSize,
        string orderBy, SortDirection sortDirection)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Product entity)
    {
        await _dbContext.Products.AddAsync(entity);
    }

    public async Task UpdateAsync(Product entity)
    {
        await Task.Factory.StartNew(() => { _dbContext.Update(entity); });
    }
}