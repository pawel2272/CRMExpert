using System.Collections.Generic;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;

namespace HRBN.Thesis.CRMExpert.UI.ViewModels.User.Task;

public class EditTaskViewModel
{
    public TodoDto Todo { get; }
    public List<ContactDataDto> ContactData { get; set; }

    public EditTaskViewModel(TodoDto todo, List<ContactDataDto> contactData)
    {
        Todo = todo;
        ContactData = contactData;
    }
}