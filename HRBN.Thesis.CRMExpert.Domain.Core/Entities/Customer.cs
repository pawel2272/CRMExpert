using System;
using System.Collections.Generic;

#nullable disable

namespace HRBN.Thesis.CRMExpert.Domain.Core.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Contacts = new HashSet<Contact>();
            Users = new HashSet<User>();
            Discounts = new HashSet<Discount>();
        }
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string TaxNo { get; set; }
        public string Regon { get; set; }
        public DateTime CreDate { get; set; }
        public DateTime ModDate { get; set; }
        
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
    }
}
