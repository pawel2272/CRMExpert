using System;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;

namespace HRBN.Thesis.CRMExpert.Domain.Core.Repositories
{
    public interface ITodosRepository : IRepository<Todo>
    {
        Task<IPageResult<Todo>> SearchAsync(Guid contactId, Guid userId, string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection);
    }
}
