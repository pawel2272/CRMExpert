using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application;
using HRBN.Thesis.CRMExpert.Application.Core.Mediator;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Role;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Role;
using HRBN.Thesis.CRMExpert.UI.Filters;
using HRBN.Thesis.CRMExpert.UI.ViewModels.Administrator.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.Administrator.Controllers;

[Area("Administrator")]
[ServiceFilter(typeof(JwtAuthFilter))]
[Authorize]
public class RoleController : Controller
{
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private Guid GetCurrentUserId()
        {
            return Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                .Value);
        }

        public async Task<IActionResult> Index(SearchRolesQuery query)
        {
            var queryToBeProcessed = query;
            if (queryToBeProcessed.PageNumber <= 0 || queryToBeProcessed.PageSize <= 0)
            {
                queryToBeProcessed = new SearchRolesQuery()
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

        public async Task<RoleViewModel> GetRoleViewModelAsync(Guid? roleId)
        {
            GetRoleQuery query = null;
            RoleDto result = null;
            
            if (roleId.HasValue)
            {
                query = new GetRoleQuery(roleId.Value);
                result = await _mediator.QueryAsync(query);
            }
            else
            {
                result = new RoleDto();
            }

            return new RoleViewModel(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await GetRoleViewModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm(Name = "Role")] EditRoleCommand command)
        {
            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateViewModelValidation(result.Errors, "Role");
                return View(await GetRoleViewModelAsync(command.Id));
            }

            return Redirect("/Administrator/Role");
        }
        
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = await GetRoleViewModelAsync(null);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm(Name = "Role")] AddRoleCommand command)
        {
            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateViewModelValidation(result.Errors, "Role");
                return View(await GetRoleViewModelAsync(null));
            }

            return Redirect("/Administrator/Role");
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteRoleCommand(id);

            var result = await _mediator.CommandAsync(command);

            return Redirect("/User/Role");
        }
}