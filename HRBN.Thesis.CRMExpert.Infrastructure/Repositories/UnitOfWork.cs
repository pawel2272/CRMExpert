using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;

namespace HRBN.Thesis.CRMExpert.Infrastructure.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly CRMContext _context;

        public UnitOfWork(CRMContext context, IPasswordHasher<User> hasher, JwtOptions jwtOptions, IHttpContextAccessor contextAccessor, IDistributedCache distributedCache)
        {
            _context = context;
            ContactsRepository = new ContactsRepository(context);
            CustomersRepository = new CustomersRepository(context);
            DiscountsRepository = new DiscountsRepository(context);
            OrdersRepository = new OrdersRepository(context);
            PermissionsRepository = new PermissionsRepository(context);
            ProductsRepository = new ProductsRepository(context);
            RolesRepository = new RolesRepository(context);
            TodosRepository = new TodosRepository(context);
            TokenRepository = new TokenRepository(contextAccessor, jwtOptions, distributedCache);
            UsersRepository = new UsersRepository(context, hasher, jwtOptions, contextAccessor, TokenRepository);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IContactsRepository ContactsRepository { get; }
        public ICustomersRepository CustomersRepository { get; }
        public IDiscountsRepository DiscountsRepository { get; }
        public IOrdersRepository OrdersRepository { get; }
        public IPermissionsRepository PermissionsRepository { get; }
        public IProductsRepository ProductsRepository { get; }
        public IRolesRepository RolesRepository { get; }
        public ITodosRepository TodosRepository { get; }
        public IUsersRepository UsersRepository { get; }
        public ITokenRepository TokenRepository { get; }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
