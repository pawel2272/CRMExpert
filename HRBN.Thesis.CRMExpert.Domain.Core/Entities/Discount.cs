using System;

#nullable disable

namespace HRBN.Thesis.CRMExpert.Domain.Core.Entities
{
    public partial class Discount
    {
        public Guid Id { get; set; }
        public decimal DiscountVaule { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime CreDate { get; set; }
        public DateTime ModDate { get; set; }

        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
    }
}