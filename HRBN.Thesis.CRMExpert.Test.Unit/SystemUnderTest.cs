using System;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Test.Unit.Models;

namespace HRBN.Thesis.CRMExpert.Test.Unit
{
    public class SystemUnderTest : IDisposable
    {
        public void Dispose()
        {
        }

        public Contact CreateContact()
        {
            var contact = new ContactProxy("Jan", "Kowalski", "123456789", "jan@kowalski.pl", "Miodowa 12", "00-000",
                "Warszawa", "Sample comment");

            contact.AddOrder("Test", "Test", 1.1m, 100);
            contact.AddOrder("Test2", "Test2", 3.14m, 100);

            contact.AddTodo("Test", "Test");
            contact.AddTodo("Test2", "Test2");

            return contact;
        }

        public Customer CreateCustomer()
        {
            var customer = new CustomerProxy("Januszex", "Kwiatowa 21", "02-737", "Warszawa", "9211233122",
                "1234567890");

            customer.AddDiscount(CreateDiscount());
            customer.AddUser(CreateUser());
            customer.Id = Guid.NewGuid();

            return customer;
        }

        public Discount CreateDiscount()
        {
            var discount = new DiscountProxy(0.3m);
            
            discount.Id = Guid.NewGuid();

            return discount;
        }

        public Order CreateOrder()
        {
            var order = new OrderProxy("Test", "Content", 21.20m, 1000);

            return order;
        }

        public Role CreateRole()
        {
            var role = new RoleProxy("Admin");

            role.AddPermission(CreatePermission());

            return role;
        }

        public Permission CreatePermission()
        {
            var permission = new PermissionProxy();
            
            permission.Id = Guid.NewGuid();

            return permission;
        }

        public Product CreateProduct()
        {
            var product = new ProductProxy("Pralka", 199.99m, "Pralka do prania", "Elektronika", 10000);
            
            product.Id = Guid.NewGuid();

            return product;
        }

        public Todo CreateTodo()
        {
            var todo = new TodoProxy("Test", "Content");
            
            todo.AddContact(CreateContact());

            return todo;
        }

        public User CreateUser()
        {
            var user = new UserProxy("admin", "M", "password", "Jan", "Kowalski", "123456789", "jan@kowalski.pl",
                "Miodowa 12", "00-000", "Warszawa");
            
            user.AddContact(CreateContact());

            return user;
        }
    }
}