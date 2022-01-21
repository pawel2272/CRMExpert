using System;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
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
        throw new NotImplementedException();
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