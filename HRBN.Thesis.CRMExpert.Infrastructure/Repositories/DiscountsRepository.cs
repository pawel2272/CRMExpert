using System;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

namespace HRBN.Thesis.CRMExpert.Infrastructure.Repositories;

public class DiscountsRepository : IDiscountsRepository
{
    private readonly CRMContext _dbContext;

    public DiscountsRepository(CRMContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Discount> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Discount entity)
    {
        throw new NotImplementedException();
    }

    public Task<IPageResult<Discount>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy,
        SortDirection sortDirection)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Discount entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Discount entity)
    {
        throw new NotImplementedException();
    }
}