using System.Collections.Generic;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;

namespace HRBN.Thesis.CRMExpert.UI.ViewModels.Administrator.Contact;

public class ContactViewModel
{
    public ContactDto Contact { get; }
    public List<UserDataDto> UserData { get; }
    public List<CustomerDataDto> CustomerData { get; }

    public ContactViewModel(ContactDto contact, List<UserDataDto> userData, List<CustomerDataDto> customerData)
    {
        Contact = contact;
        UserData = userData;
        CustomerData = customerData;
    }
}