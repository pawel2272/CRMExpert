using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

namespace HRBN.Thesis.CRMExpert.UI.ViewModels.Administrator.User;

public class UserViewModel
{
    public UserDto User { get; }

    public UserViewModel(UserDto user)
    {
        User = user;
    }
}