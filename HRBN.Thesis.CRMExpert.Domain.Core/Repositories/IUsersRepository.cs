using System;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;

namespace HRBN.Thesis.CRMExpert.Domain.Core.Repositories
{
    public interface IUsersRepository
    {
        Task<User> GetAsync(Guid id);
        Task DeleteAsync(User user);
        Task<IPageResult<User>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<string> LoginAsync(string username, string password, bool rememberMe);
        Task LogoutAsync();
    }
}
