using System.Collections.Generic;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;

namespace HRBN.Thesis.CRMExpert.UI.ViewModels.Administrator.Permission;

public class PermissionViewModel
{
    public PermissionDto Permission { get; }
    public List<UserDataDto> UserData { get; }
    public List<RoleDto> RoleData { get; }

    public PermissionViewModel(PermissionDto permission, List<UserDataDto> userData, List<RoleDto> roleData)
    {
        Permission = permission;
        UserData = userData;
        RoleData = roleData;
    }
}