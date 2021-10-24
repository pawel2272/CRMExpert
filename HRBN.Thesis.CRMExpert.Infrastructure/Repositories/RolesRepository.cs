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

        public async Task AddAsync(Role role)
        {
            await _dbContext.Roles.AddAsync(role);
        }

        public async Task DeleteAsync(Role role)
        {
            _dbContext.Roles.Remove(role);
        }

        public async Task<Role> GetAsync(Guid id)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
            return role;
        }

        public async Task<IPageResult<Role>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            var baseQuery = _dbContext.Roles
                .Where(r => searchPhrase == null ||
                                r.Name.ToLower().Contains(searchPhrase.ToLower()));
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Role, object>>>()
                {
                    { nameof(Role.Name), r => r.Name },
                };

                var selectedColumn = columnSelectors[orderBy];

                baseQuery = sortDirection == SortDirection.ASC ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
            }
            var orders = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return new PageResult<Role>(orders, baseQuery.Count(), pageSize, pageNumber);
        }

        public async Task UpdateAsync(Role role)
        {
            _dbContext.Roles.Update(role);
        }
    }
}
