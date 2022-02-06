using System.Collections.Generic;
using System.Linq;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Todo;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Domain.Core.Dto;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;

namespace HRBN.Thesis.CRMExpert.UI.Models;

public class TaskViewModel
{
    public AddTodoCommand Todo { get; }
    public PageResult<TodoDto> Todos { get; }
    public List<ContactDataDto> ContactData { get; set; }
    public string CustomerName { get; set; }

    public TaskViewModel(AddTodoCommand todo, PageResult<TodoDto> todos, List<ContactDataDto> contactData)
    {
        Todo = todo;
        Todos = todos;
        ContactData = contactData;
        if (Todos.Items.Count > 0)
        {
            CustomerName = Todos.Items.FirstOrDefault(e => !string.IsNullOrEmpty(e.CustomerName)).CustomerName;
        }
    }
}