using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

namespace HRBN.Thesis.CRMExpert.UI.ViewModels.User.Customer;

public class CustomerViewModel
{
    public CustomerDto Customer { get; }

    public CustomerViewModel(CustomerDto customer)
    {
        Customer = customer;
    }
}