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
    public sealed class TodosRepository : ITodosRepository
    {
        private readonly CRMContext _dbContext;

        public TodosRepository(CRMContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Todo entity)
        {
            await _dbContext.Todos.AddAsync(entity);
        }

        public async Task DeleteAsync(Todo entity)
        {
            await Task.Factory.StartNew(() => { _dbContext.Todos.Remove(entity); });
        }

        public async Task<Todo> GetAsync(Guid id)
        {
            var todo = await _dbContext.Todos.FirstOrDefaultAsync(e => e.Id == id);
            return todo;
        }

        private async Task<IPageResult<Todo>> ProcessSearchQueryAsync(IQueryable<Todo> baseQuery, int pageNumber,
            int pageSize, string orderBy, SortDirection sortDirection,
            string searchPhrase)
        {
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Todo, object>>>()
                {
                    {nameof(Todo.Title), e => e.Title},
                    {nameof(Todo.Content), e => e.Content},
                    {nameof(Todo.CreDate), e => e.CreDate},
                    {nameof(Todo.ModDate), e => e.ModDate}
                };

                var selectedColumn = columnSelectors[orderBy];

                baseQuery = sortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var entities = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Include(e => e.Contact)
                .ToListAsync();

            return new PageResult<Todo>(entities, baseQuery.Count(), pageSize, pageNumber, searchPhrase, sortDirection, orderBy);
        }

        public async Task<IPageResult<Todo>> SearchAsync(string searchPhrase, int pageNumber, int pageSize,
            string orderBy,
            SortDirection sortDirection)
        {
            string lowerCaseSearchPhrase = searchPhrase?.ToLower();

            var baseQuery = _dbContext.Todos
                .Where(e => searchPhrase == null ||
                            (e.Id.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                             || e.Title.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.Content.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.ContactId.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                             || e.UserId.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                            ));

            return await ProcessSearchQueryAsync(baseQuery, pageNumber, pageSize, orderBy, sortDirection, searchPhrase);
        }

        public async Task<IPageResult<Todo>> SearchAsync(Guid contactId, Guid userId, string searchPhrase, int pageNumber,
            int pageSize, string orderBy, SortDirection sortDirection)
        {
            string lowerCaseSearchPhrase = searchPhrase?.ToLower();

            var baseQuery = _dbContext.Todos
                .Where(e => e.ContactId == contactId && e.UserId == userId &&
                            (searchPhrase == null ||
                             (e.Id.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                              || e.Title.ToLower().Contains(lowerCaseSearchPhrase)
                              || e.Content.ToLower().Contains(lowerCaseSearchPhrase)
                              || e.ContactId.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                              || e.UserId.ToString().ToLower().Contains(lowerCaseSearchPhrase)
                             )));

            return await ProcessSearchQueryAsync(baseQuery, pageNumber, pageSize, orderBy, sortDirection, searchPhrase);
        }

        public async Task UpdateAsync(Todo entity)
        {
            await Task.Factory.StartNew(() => { _dbContext.Todos.Update(entity); });
        }
    }
}