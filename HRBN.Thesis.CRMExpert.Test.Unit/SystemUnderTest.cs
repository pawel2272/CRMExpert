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
            var contact = new ContactProxy("Jan", "Kowalski", "123456789", "jan@kowalski.pl", "Miodowa 12", "00-000", "Warszawa", "Sample comment");

            contact.AddOrder("Test", "Test", 1.1m, 100);
            contact.AddOrder("Test2", "Test2", 3.14m, 100);

            contact.AddTodo("Test", "Test");
            contact.AddTodo("Test2", "Test2");

            return contact;
        }

        public Order CreateOrder()
        {
            var order = new OrderProxy("Test", "Content", 21.20m, 1000);
            order.AddContact(CreateContact());
            return order;
        }

        public Role CreateRole()
        {
            var role = new RoleProxy("Admin");
            
            // role.AddPermission();
            
            return role;
        }

        public Todo CreateTodo()
        {
            var todo = new TodoProxy("Test", "Content");
            todo.AddContact(CreateContact());
            return todo;
        }

        public User CreateUser()
        {
            var user = new UserProxy("admin", "M", "password", "Jan", "Kowalski", "123456789", "jan@kowalski.pl", "Miodowa 12", "00-000", "Warszawa");

            // user.AddPermission();
            // user.AddRole("Admin");
            user.AddContact(CreateContact());

            return user;
        }
    }
}
