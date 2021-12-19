using System;
using System.Threading.Tasks;

namespace HRBN.Thesis.CRMExpert.Domain.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IContactsRepository ContactsRepository { get; }
        ICustomersRepository CustomersRepository { get; }
        IDiscountsRepository DiscountsRepository { get; }
        IOrdersRepository OrdersRepository { get; }
        IPermissionsRepository PermissionsRepository { get; }
        IProductsRepository ProductsRepository { get; }
        IRolesRepository RolesRepository { get; }
        ITodosRepository TodosRepository { get; }
        IUsersRepository UsersRepository { get; }
        ITokenRepository TokenRepository { get; }
        Task CommitAsync();
    }
}