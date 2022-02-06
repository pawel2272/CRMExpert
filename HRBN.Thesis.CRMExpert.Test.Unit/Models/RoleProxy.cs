using System;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Models
{
    public class RoleProxy : Role
    {
        public RoleProxy(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public void AddPermission(Permission permission)
        {
            this.Permissions.Add(permission);
        }
    }
}
