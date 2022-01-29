using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

namespace HRBN.Thesis.CRMExpert.UI.ViewModels.Administrator.Role;

public class RoleViewModel
{
    public RoleDto Role { get; }

    public RoleViewModel(RoleDto role)
    {
        Role = role;
    }
}