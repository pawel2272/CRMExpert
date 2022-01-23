using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application;
using HRBN.Thesis.CRMExpert.Application.Core.Mediator;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Contact;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Contact;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Customer;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.User;
using HRBN.Thesis.CRMExpert.UI.Filters;
using HRBN.Thesis.CRMExpert.UI.ViewModels.Administrator.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.User.Controllers
{
    [Area("User")]
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class ContactController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private Guid GetCurrentUserId()
        {
            return Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                .Value);
        }

        public async Task<IActionResult> Index(SearchContactsQuery query)
        {
            var queryToBeProcessed = query;
            if (queryToBeProcessed.PageNumber <= 0 || queryToBeProcessed.PageSize <= 0)
            {
                queryToBeProcessed = new SearchContactsQuery()
                {
                    UserId = query.UserId,
                    SearchPhrase = query.SearchPhrase,
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = query.OrderBy,
                    SortDirection = query.SortDirection
                };
            }

            if (queryToBeProcessed.UserId.Equals(Guid.Empty))
            {
                var userId = GetCurrentUserId();
                queryToBeProcessed.UserId = userId;
            }

            ViewBag.PageNumber = queryToBeProcessed.PageNumber;

            var contacts = await _mediator.QueryAsync(queryToBeProcessed);
            return View(contacts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var query = new GetContactQuery(id);

            var result = await _mediator.QueryAsync(query);

            var userData = await _mediator.QueryAsync(new GetUserDataQuery());
            var customerData = await _mediator.QueryAsync(new GetCustomerDataQuery());

            var model = new ContactViewModel(result, userData, customerData);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditContactCommand command)
        {
            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View();
            }

            return Redirect("/User/Contact");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddContactCommand command)
        {
            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View(nameof(this.Index));
            }

            return Redirect("/User/Contact");
        }
    }
}