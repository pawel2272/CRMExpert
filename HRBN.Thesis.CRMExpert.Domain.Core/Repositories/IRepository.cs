using System;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;

namespace HRBN.Thesis.CRMExpert.Domain.Core.Repositories;

public interface IRepository<T>
{
    Task<T> GetAsync(Guid id);
    Task DeleteAsync(T entity);
    Task<IPageResult<T>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy,
        SortDirection sortDirection);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
}