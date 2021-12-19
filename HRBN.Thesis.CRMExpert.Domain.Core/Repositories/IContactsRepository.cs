using System;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;

namespace HRBN.Thesis.CRMExpert.Domain.Core.Repositories
{
    public interface IContactsRepository : IRepository<Contact>
    {
        Task<IPageResult<Contact>> SearchAsync(Guid userId, string searchPhrase, int pageNumber, int pageSize,
            string orderBy, SortDirection sortDirection);
    }
}