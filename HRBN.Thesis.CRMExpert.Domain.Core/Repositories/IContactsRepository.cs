using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;

namespace HRBN.Thesis.CRMExpert.Domain.Core.Repositories
{
    public interface IContactsRepository : IRepository<Contact>
    {
        Task<IPageResult<Contact>> SearchAsync(Guid userId, string searchPhrase, int pageNumber, int pageSize,
            string orderBy, SortDirection sortDirection);
        Task<List<ContactDataDto>> GetContactDataAsync();
        Task<List<ContactDataDto>> GetContactDataAsyncByCustomer(Guid customerId);
        Task<List<ContactDataDto>> GetContactDataAsyncByUser(Guid userId);
        Task<List<Contact>> GetContactByUserAsync(Guid userId);
    }
}