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
        await Task.Factory.StartNew(() => { _dbContext.Permissions.Remove(entity); });
    }

    public async Task<IPageResult<Permission>> SearchAsync(string searchPhrase, int pageNumber, int pageSize,
        string orderBy, SortDirection sortDirection)
    {
        string lowerCaseSearchPhrase = searchPhrase?.ToLower();

        var baseQuery = _dbContext.Permissions
            .Include(e => e.User)
            .Include(e => e.Role)
            .Where(e => (searchPhrase == null ||
                         (e.Id.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                          || e.UserId.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                          || e.RoleId.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                          || e.User.Username.ToLower().Contains(lowerCaseSearchPhrase)
                          || e.Role.Name.ToLower().Contains(lowerCaseSearchPhrase))));
        if (!string.IsNullOrEmpty(orderBy))
        {
            var columnSelectors = new Dictionary<string, Expression<Func<Permission, object>>>()
            {
                {nameof(Permission.CreDate), e => e.CreDate},
                {nameof(Permission.ModDate), e => e.ModDate},
                {nameof(User.Username), e => e.User.Username},
                {nameof(Role.Name), e => e.Role.Name}
            };

            Expression<Func<Permission, object>> selectedColumn;

            if (columnSelectors.Keys.Contains(orderBy))
            {
                selectedColumn = columnSelectors[orderBy];
            }
            else
            {
                selectedColumn = columnSelectors["CreDate"];
            }

            baseQuery = sortDirection == SortDirection.ASC
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var entities = await baseQuery.Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return new PageResult<Permission>(entities, baseQuery.Count(), pageSize, pageNumber, searchPhrase,
            sortDirection, orderBy);
    }

    public async Task AddAsync(Permission entity)
    {
        await _dbContext.Permissions.AddAsync(entity);
    }

    public async Task UpdateAsync(Permission entity)
    {
        await Task.Factory.StartNew(() => { _dbContext.Permissions.Update(entity); });
    }
}