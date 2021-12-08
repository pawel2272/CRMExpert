using System;
using System.Collections.Generic;

#nullable disable

namespace HRBN.Thesis.CRMExpert.Domain.Core.Entities
{
    public partial class Role
    {
        public Role()
        {
            Permissions = new HashSet<Permission>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreDate { get; set; }
        public DateTime ModDate { get; set; }
        
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
