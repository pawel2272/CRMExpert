using System;
using System.Collections.Generic;

#nullable disable

namespace HRBN.Thesis.CRMExpert.Domain.Core.Entities
{
    public partial class User
    {
        public User()
        {
            Contacts = new HashSet<Contact>();
            Permissions = new HashSet<Permission>();
            Todos = new HashSet<Todo>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public DateTime CreDate { get; set; }
        public DateTime ModDate { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<Todo> Todos { get; set; }
    }
}
