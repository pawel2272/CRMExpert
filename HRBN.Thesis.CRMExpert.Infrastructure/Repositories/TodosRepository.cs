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

        public async Task AddAsync(Todo todo)
        {
            await _dbContext.Todos.AddAsync(todo);
        }

        public async Task DeleteAsync(Todo todo)
        {
            _dbContext.Todos.Remove(todo);
        }

        public async Task<Todo> GetAsync(Guid id)
        {
            var todo = await _dbContext.Todos.FirstOrDefaultAsync(t => t.Id == id);
            return todo;
        }

        public async Task<IPageResult<Todo>> SearchAsync(Guid contactId, string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            var baseQuery = _dbContext.Todos
                .Where(t => t.ContactId == contactId &&
                            (searchPhrase == null ||
                             (t.Title.ToLower().Contains(searchPhrase.ToLower())
                              || t.Content.ToLower().Contains(searchPhrase.ToLower()))));
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Todo, object>>>()
                {
                    { nameof(Todo.Title), o => o.Title },
                    { nameof(Todo.Content), o => o.Content }
                };

                var selectedColumn = columnSelectors[orderBy];

                baseQuery = sortDirection == SortDirection.ASC ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
            }
            var todos = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<Todo>(todos, baseQuery.Count(), pageSize, pageNumber);
        }

        public async Task UpdateAsync(Todo todo)
        {
            _dbContext.Todos.Update(todo);
        }
    }
}
