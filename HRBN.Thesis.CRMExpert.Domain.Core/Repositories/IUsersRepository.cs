using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;

namespace HRBN.Thesis.CRMExpert.Domain.Core.Repositories
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<string> LoginAsync(string username, string password, bool rememberMe);
        Task LogoutAsync();
        Task<List<UserDataDto>> GetUserDataAsync();
        Task<bool> IsPasswordValid(Guid id, string givenPassword);
    }
}