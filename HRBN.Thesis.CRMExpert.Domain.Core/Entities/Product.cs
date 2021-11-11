using System;
using System.Collections.Generic;

#nullable disable

namespace HRBN.Thesis.CRMExpert.Domain.Core.Entities
{
    public partial class Product
    {
        public Product()
        {
            Discounts = new HashSet<Discount>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public decimal Count { get; set; }
        public DateTime CreDate { get; set; }
        public DateTime ModDate { get; set; }
        
        public ICollection<Discount> Discounts { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}