using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application;
using HRBN.Thesis.CRMExpert.Application.Core.Mediator;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Todo;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Contact;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Todo;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.UI.Filters;
using HRBN.Thesis.CRMExpert.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.User.Controllers;

[Area("User")]
[ServiceFilter(typeof(JwtAuthFilter))]
[Authorize]
public class TaskController : Controller
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private Guid GetCurrentUserId()
    {
        return Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
            .Value);
    }

    public async Task<TaskViewModel> GetTodoViewModelAsync(Guid contactId)
    {
        var query = new SearchTodosQuery()
        {
            ContactId = contactId,
            UserId = GetCurrentUserId(),
            OrderBy = "Title",
            PageNumber = 1,
            PageSize = 1000000,
            SearchPhrase = null,
            SortDirection = SortDirection.ASC
        };

        var result = await _mediator.QueryAsync(query);

        var contactData = await _mediator.QueryAsync(new GetContactDataQuery());


        var viewModel = new TaskViewModel(new AddTodoCommand(), result, contactData);

        return viewModel;
    }
    
    public async Task<EditTaskViewModel> GetEditTodoViewModelAsync(Guid id)
    {
        var query = new GetTodoQuery(id);

        var result = await _mediator.QueryAsync(query);

        var contactData = await _mediator.QueryAsync(new GetContactDataQuery());
        
        var viewModel = new EditTaskViewModel(result, contactData);

        return viewModel;
    }

    [HttpGet]
    public async Task<IActionResult> Index(Guid contactId)
    {
        return View(await GetTodoViewModelAsync(contactId));
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm(Name = "Todo")] AddTodoCommand command)
    {
        command.UserId = GetCurrentUserId();

        var result = await _mediator.CommandAsync(command);

        if (result.IsFailure)
        {
            ModelState.PopulateViewModelValidation(result.Errors, "Todo");
            View("Index", await GetTodoViewModelAsync(command.ContactId.Value));
        }

        return RedirectToAction("Index", new {contactId = command.ContactId.Value});
    }

    [HttpGet]
    public async Task<IActionResult> Select()
    {
        var query = new GetContactDataQuery();

        var result = await _mediator.QueryAsync(query);
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Select(Guid id)
    {
        return await Task.Factory.StartNew(() =>
        {
            return RedirectToAction("Index", new {contactId = id});
        });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id, Guid contactId)
    {
        var command = new DeleteTodoCommand(id, contactId);

        var result = await _mediator.CommandAsync(command);

        return RedirectToAction("Index", new {contactId = contactId});
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        return View(await GetEditTodoViewModelAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromForm(Name = "Todo")] EditTodoCommand command)
    {
        command.UserId = GetCurrentUserId();
        
        var result = await _mediator.CommandAsync(command);

        if (result.IsFailure)
        {
            ModelState.PopulateViewModelValidation(result.Errors, "Todo");
            return View(await GetEditTodoViewModelAsync(command.Id));
        }

        return RedirectToAction("Index", new {contactId = command.ContactId});
    }
}