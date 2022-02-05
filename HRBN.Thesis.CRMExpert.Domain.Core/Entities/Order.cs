using System;
using System.Collections.Generic;

#nullable disable

namespace HRBN.Thesis.CRMExpert.Domain.Core.Entities
{
    public partial class Order
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public decimal Count { get; set; }
        public Guid ContactId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreDate { get; set; }
        public DateTime ModDate { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual Product Product { get; set; }
    }
}
