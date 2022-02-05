using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;

namespace HRBN.Thesis.CRMExpert.Domain.Core.Repositories
{
    public interface IOrdersRepository : IRepository<Order>
    {
        Task<IPageResult<Order>> SearchAsync(Guid contactId, string searchPhrase, int pageNumber, int pageSize,
            string orderBy, SortDirection sortDirection);

        Task<List<Order>> GetLastOrders(int daysFromToday);
    }
}