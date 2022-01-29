using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application;
using HRBN.Thesis.CRMExpert.Application.Core.Mediator;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Permission;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Permission;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Role;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.User;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.UI.Filters;
using HRBN.Thesis.CRMExpert.UI.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.Administrator.Controllers;

[Area("Administrator")]
[ServiceFilter(typeof(JwtAuthFilter))]
[Authorize]
public class PermissionController : Controller
{
        private readonly IMediator _mediator;

        public PermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private Guid GetCurrentUserId()
        {
            return Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                .Value);
        }

        public async Task<IActionResult> Index(SearchPermissionsQuery query)
        {
            var queryToBeProcessed = query;
            if (queryToBeProcessed.PageNumber <= 0 || queryToBeProcessed.PageSize <= 0)
            {
                queryToBeProcessed = new SearchPermissionsQuery()
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

        public async Task<PermissionViewModel> GetPermissionViewModelAsync(Guid? permissionId)
        {
            GetPermissionQuery query = null;
            PermissionDto result = null;
            
            if (permissionId.HasValue)
            {
                query = new GetPermissionQuery(permissionId.Value);
                result = await _mediator.QueryAsync(query);
            }
            else
            {
                result = new PermissionDto();
            }

            var userData = await _mediator.QueryAsync(new GetUserDataQuery());
            var roleData = await _mediator.QueryAsync(new SearchRolesQuery()
            {
                OrderBy = "Name",
                PageNumber = 1,
                PageSize = 10000,
                SearchPhrase = null,
                SortDirection = SortDirection.ASC
            });

            return new PermissionViewModel(result, userData, roleData.Items);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await GetPermissionViewModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm(Name = "Permission")] EditPermissionCommand command)
        {
            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateViewModelValidation(result.Errors, "Permission");
                return View(await GetPermissionViewModelAsync(command.Id));
            }

            return Redirect("/Administrator/Permission");
        }
        
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = await GetPermissionViewModelAsync(null);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm(Name = "Permission")] AddPermissionCommand command)
        {
            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateViewModelValidation(result.Errors, "Permission");
                return View(await GetPermissionViewModelAsync(null));
            }

            return Redirect("/Administrator/Permission");
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeletePermissionCommand(id);

            var result = await _mediator.CommandAsync(command);

            return Redirect("/User/Permission");
        }
}