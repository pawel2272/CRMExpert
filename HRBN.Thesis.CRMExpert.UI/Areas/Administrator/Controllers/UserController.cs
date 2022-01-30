using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application;
using HRBN.Thesis.CRMExpert.Application.Core.Mediator;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.User;
using HRBN.Thesis.CRMExpert.UI.Filters;
using HRBN.Thesis.CRMExpert.UI.ViewModels.Administrator.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.Administrator.Controllers;

[Area("Administrator")]
[ServiceFilter(typeof(JwtAuthFilter))]
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private Guid GetCurrentUserId()
    {
        return Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
            .Value);
    }

    public async Task<IActionResult> Index(SearchUsersQuery query)
    {
        var queryToBeProcessed = query;
        if (queryToBeProcessed.PageNumber <= 0 || queryToBeProcessed.PageSize <= 0)
        {
            queryToBeProcessed = new SearchUsersQuery()
            {
                SearchPhrase = query.SearchPhrase,
                PageNumber = 1,
                PageSize = 10,
                OrderBy = query.OrderBy,
                SortDirection = query.SortDirection
            };
        }

        var entities = await _mediator.QueryAsync(queryToBeProcessed);
        return View(entities);
    }

    public async Task<UserViewModel> GetUserViewModelAsync(Guid? userId)
    {
        GetUserQuery query = null;
        UserDto result = null;

        if (userId.HasValue)
        {
            query = new GetUserQuery(userId.Value);
            result = await _mediator.QueryAsync(query);
        }
        else
        {
            result = new UserDto();
        }

        return new UserViewModel(result);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var model = await GetUserViewModelAsync(id);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromForm(Name = "User")] EditUserCommand command)
    {
        var result = await _mediator.CommandAsync(command);

        if (result.IsFailure)
        {
            ModelState.PopulateViewModelValidation(result.Errors, "User");
            return View(await GetUserViewModelAsync(command.Id));
        }

        return Redirect("/Administrator/User");
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = await GetUserViewModelAsync(null);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm(Name = "User")] AddUserCommand command)
    {
        var result = await _mediator.CommandAsync(command);

        if (result.IsFailure)
        {
            ModelState.PopulateViewModelValidation(result.Errors, "User");
            return View(await GetUserViewModelAsync(null));
        }

        return Redirect("/Administrator/User");
    }


    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteUserCommand(id);

        var result = await _mediator.CommandAsync(command);

        return Redirect("/User/User");
    }
}