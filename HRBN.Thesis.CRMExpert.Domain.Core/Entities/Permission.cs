using System;

#nullable disable

namespace HRBN.Thesis.CRMExpert.Domain.Core.Entities
{
    public partial class Permission
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? RoleId { get; set; }
        public DateTime CreDate { get; set; }
        public DateTime ModDate { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}