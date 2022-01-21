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

public class DiscountsRepository : IDiscountsRepository
{
    private readonly CRMContext _dbContext;

    public DiscountsRepository(CRMContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Discount> GetAsync(Guid id)
    {
        return await _dbContext.Discounts.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task DeleteAsync(Discount entity)
    {
        await Task.Factory.StartNew(() => { _dbContext.Discounts.Remove(entity); });
    }

    public async Task<IPageResult<Discount>> SearchAsync(string searchPhrase, int pageNumber, int pageSize,
        string orderBy,
        SortDirection sortDirection)
    {
        string lowerCaseSearchPhrase = searchPhrase?.ToLower();

        var baseQuery = _dbContext.Discounts
            .Where(c => (searchPhrase == null ||
                         c.Id.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                         || c.DiscountVaule.ToString().Contains(lowerCaseSearchPhrase)
                         || c.ProductId.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                         || c.CustomerId.ToString().ToLower().Contains(lowerCaseSearchPhrase))
            );
        if (!string.IsNullOrEmpty(orderBy))
        {
            var columnSelectors = new Dictionary<string, Expression<Func<Discount, object>>>()
            {
                {nameof(Discount.DiscountVaule), e => e.DiscountVaule},
                {nameof(Discount.CreDate), e => e.CreDate},
                {nameof(Discount.ModDate), e => e.ModDate}
            };

            Expression<Func<Discount, object>> selectedColumn;

            if (columnSelectors.Keys.Contains(orderBy))
            {
                selectedColumn = columnSelectors[orderBy];
            }
            else
            {
                selectedColumn = columnSelectors["DiscountVaule"];
            }

            baseQuery = sortDirection == SortDirection.ASC
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var entities = await baseQuery.Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return new PageResult<Discount>(entities, baseQuery.Count(), pageSize, pageNumber);
    }

    public async Task AddAsync(Discount entity)
    {
        await _dbContext.Discounts.AddAsync(entity);
    }

    public async Task UpdateAsync(Discount entity)
    {
        await Task.Factory.StartNew(() => { _dbContext.Discounts.Update(entity); });
    }
}