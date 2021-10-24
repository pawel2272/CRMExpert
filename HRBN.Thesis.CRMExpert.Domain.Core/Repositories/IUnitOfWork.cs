using System;
using System.Threading.Tasks;

namespace HRBN.Thesis.CRMExpert.Domain.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IContactsRepository ContactsRepository { get; }
        IOrdersRepository OrdersRepository { get; }
        IRolesRepository RolesRepository { get; }
        ITodosRepository TodosRepository { get; }
        IUsersRepository UsersRepository { get; }
        ITokenRepository TokenRepository { get; }
        Task CommitAsync();
    }
}