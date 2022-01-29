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

namespace HRBN.Thesis.CRMExpert.Infrastructure.Repositories
{
    public sealed class RolesRepository : IRolesRepository
    {
        private readonly CRMContext _dbContext;

        public RolesRepository(CRMContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Role entity)
        {
            await _dbContext.Roles.AddAsync(entity);
        }

        public async Task DeleteAsync(Role entity)
        {
            await Task.Factory.StartNew(() => { _dbContext.Roles.Remove(entity); });
        }

        public async Task<Role> GetAsync(Guid id)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(e => e.Id == id);
            return role;
        }

        public async Task<IPageResult<Role>> SearchAsync(string searchPhrase, int pageNumber, int pageSize,
            string orderBy, SortDirection sortDirection)
        {
            string lowerCaseSearchPhrase = searchPhrase?.ToLower();

            var baseQuery = _dbContext.Roles
                .Where(e => searchPhrase == null ||
                            e.Id.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                            || e.Name.ToLower().Contains(lowerCaseSearchPhrase)
                );
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Role, object>>>()
                {
                    {nameof(Role.Name), e => e.Name},
                    {nameof(Role.CreDate), e => e.CreDate},
                    {nameof(Role.ModDate), e => e.ModDate}
                };

                var selectedColumn = columnSelectors[orderBy];

                baseQuery = sortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var entities = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return new PageResult<Role>(entities, baseQuery.Count(), pageSize, pageNumber, searchPhrase, sortDirection, orderBy);
        }

        public async Task UpdateAsync(Role entity)
        {
            await Task.Factory.StartNew(() => { _dbContext.Roles.Update(entity); });
        }
    }
}