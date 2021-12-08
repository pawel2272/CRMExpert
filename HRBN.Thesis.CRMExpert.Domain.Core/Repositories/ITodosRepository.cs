using System;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;

namespace HRBN.Thesis.CRMExpert.Domain.Core.Repositories
{
    public interface ITodosRepository
    {
        Task<Todo> GetAsync(Guid id);
        Task DeleteAsync(Todo todo);
        Task<IPageResult<Todo>> SearchAsync(Guid contactId, string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection);
        Task AddAsync(Todo todo);
        Task UpdateAsync(Todo todo);
    }
}
