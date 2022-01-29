using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application;
using HRBN.Thesis.CRMExpert.Application.Core.Mediator;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Customer;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Customer;
using HRBN.Thesis.CRMExpert.UI.Filters;
using HRBN.Thesis.CRMExpert.UI.ViewModels.User.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.User.Controllers
{
    [Area("User")]
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class CustomerController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private Guid GetCurrentUserId()
        {
            return Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                .Value);
        }

        public async Task<IActionResult> Index(SearchCustomersQuery query)
        {
            var queryToBeProcessed = query;
            if (queryToBeProcessed.PageNumber <= 0 || queryToBeProcessed.PageSize <= 0)
            {
                queryToBeProcessed = new SearchCustomersQuery()
                {
                    SearchPhrase = query.SearchPhrase,
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = query.OrderBy,
                    SortDirection = query.SortDirection
                };
            }
            
            ViewBag.PageNumber = queryToBeProcessed.PageNumber;

            var entities = await _mediator.QueryAsync(queryToBeProcessed);
            return View(entities);
        }

        public async Task<CustomerViewModel> GetCustomerViewModelAsync(Guid? customerId)
        {
            GetCustomerQuery query = null;
            CustomerDto result = null;

            if (customerId.HasValue)
            {
                query = new GetCustomerQuery(customerId.Value);
                result = await _mediator.QueryAsync(query);
            }
            else
            {
                result = new CustomerDto();
            }

            return new CustomerViewModel(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await GetCustomerViewModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm(Name = "Customer")] EditCustomerCommand command)
        {
            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateViewModelValidation(result.Errors, "Customer");
                return View(await GetCustomerViewModelAsync(command.Id));
            }

            return Redirect("/User/Customer");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = await GetCustomerViewModelAsync(null);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm(Name = "Customer")] AddCustomerCommand command)
        {
            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateViewModelValidation(result.Errors, "Customer");
                return View(await GetCustomerViewModelAsync(null));
            }

            return Redirect("/User/Customer");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCustomerCommand(id);

            var result = await _mediator.CommandAsync(command);

            return Redirect("/User/Customer");
        }
    }
}