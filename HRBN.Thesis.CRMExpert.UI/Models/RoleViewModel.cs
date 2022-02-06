using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

namespace HRBN.Thesis.CRMExpert.UI.Models;

public class RoleViewModel
{
    public RoleDto Role { get; }

    public RoleViewModel(RoleDto role)
    {
        Role = role;
    }
}