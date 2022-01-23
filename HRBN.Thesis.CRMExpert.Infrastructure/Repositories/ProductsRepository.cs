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
        string lowerCaseSearchPhrase = searchPhrase?.ToLower();

        var baseQuery = _dbContext.Products
            .Where(e => (searchPhrase == null ||
                         (e.Id.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                          || e.Name.ToLower().Contains(lowerCaseSearchPhrase)
                          || e.Price.ToString().Contains(lowerCaseSearchPhrase)
                          || e.Description.ToLower().Contains(lowerCaseSearchPhrase)
                          || e.Type.ToLower().Contains(lowerCaseSearchPhrase)
                          || e.Count.ToString().Contains(lowerCaseSearchPhrase))));
        if (!string.IsNullOrEmpty(orderBy))
        {
            var columnSelectors = new Dictionary<string, Expression<Func<Product, object>>>()
            {
                {nameof(Product.Name), e => e.Name},
                {nameof(Product.Price), e => e.Price},
                {nameof(Product.Description), e => e.Description},
                {nameof(Product.Type), e => e.Type},
                {nameof(Product.Count), e => e.Count},
                {nameof(Product.CreDate), e => e.CreDate},
                {nameof(Product.ModDate), e => e.ModDate}
            };

            Expression<Func<Product, object>> selectedColumn;

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

        return new PageResult<Product>(entities, baseQuery.Count(), pageSize, pageNumber);
    }

    public async Task AddAsync(Product entity)
    {
        await _dbContext.Products.AddAsync(entity);
    }

    public async Task UpdateAsync(Product entity)
    {
        await Task.Factory.StartNew(() => { _dbContext.Update(entity); });
    }

    public async Task<List<ProductDataDto>> GetProductDataAsync()
    {
        return await Task.Factory.StartNew(() =>
        {
            var results = _dbContext.Products.Select(e => new ProductDataDto() {Id = e.Id, Name = e.Name}).ToList();
            return results;
        });
    }
}