using HRBN.Thesis.CRMExpert.Domain.Core.Entities;

namespace HRBN.Thesis.CRMExpert.Test.Unit.Models;

public class PermissionProxy : Permission
{
    public PermissionProxy() : base()
    {
    }

    public void AddUser(User user)
    {
        this.User = user;
        this.UserId = user.Id;
    }

    public void AddRole(Role role)
    {
        this.Role = role;
        this.RoleId = role.Id;
    }
}