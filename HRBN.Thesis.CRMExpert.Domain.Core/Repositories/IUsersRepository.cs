using System;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;

namespace HRBN.Thesis.CRMExpert.Domain.Core.Repositories
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<string> LoginAsync(string username, string password, bool rememberMe);
        Task LogoutAsync();
    }
}