using System;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HRBN.Thesis.CRMExpert.Infrastructure.Repositories;

public class PermissionsRepository : IPermissionsRepository
{
    private readonly CRMContext _dbContext;

    public PermissionsRepository(CRMContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Permission> GetAsync(Guid id)
    {
        return await _dbContext.Permissions.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task DeleteAsync(Permission entity)
    {
        await Task.Factory.StartNew(() =>
        {
            _dbContext.Permissions.Remove(entity);
        });
    }

    public async Task<IPageResult<Permission>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Permission entity)
    {
        await _dbContext.Permissions.AddAsync(entity);
    }

    public async Task UpdateAsync(Permission entity)
    {
        await Task.Factory.StartNew(() =>
        {
            _dbContext.Permissions.Update(entity);
        });
        
    }
}