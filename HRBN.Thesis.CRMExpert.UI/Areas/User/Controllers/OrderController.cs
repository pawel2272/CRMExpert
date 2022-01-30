using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application;
using HRBN.Thesis.CRMExpert.Application.Core.Mediator;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Order;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Contact;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Order;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Product;
using HRBN.Thesis.CRMExpert.UI.Filters;
using HRBN.Thesis.CRMExpert.UI.ViewModels.User.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.User.Controllers
{
    [Area("User")]
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private Guid GetCurrentUserId()
        {
            return Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                .Value);
        }

        public async Task<IActionResult> Index(SearchOrdersQuery query)
        {
            var contactData = await _mediator.QueryAsync(new GetContactDataQuery());
            
            var queryToBeProcessed = query;
            if (queryToBeProcessed.PageNumber <= 0 || queryToBeProcessed.PageSize <= 0)
            {
                queryToBeProcessed = new SearchOrdersQuery()
                {
                    ContactId = query.ContactId,
                    SearchPhrase = query.SearchPhrase,
                    PageNumber = 1,
                    PageSize = 10,
                    OrderBy = query.OrderBy,
                    SortDirection = query.SortDirection
                };
            }

            ViewBag.PageNumber = queryToBeProcessed.PageNumber;

            var entities = await _mediator.QueryAsync(queryToBeProcessed);
            entities.ContactId = query.ContactId;
            return View(entities);
        }

        public async Task<OrderViewModel> GetContactViewModelAsync(Guid? orderId)
        {
            GetOrderQuery query = null;
            OrderDto result = null;

            if (orderId.HasValue)
            {
                query = new GetOrderQuery(orderId.Value);
                result = await _mediator.QueryAsync(query);
            }
            else
            {
                result = new OrderDto();
            }

            var contactData = await _mediator.QueryAsync(new GetContactDataQuery());
            var productData = await _mediator.QueryAsync(new GetProductDataQuery());

            return new OrderViewModel(result, contactData, productData);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await GetContactViewModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm(Name = "Order")] EditOrderCommand command)
        {
            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateViewModelValidation(result.Errors, "Order");
                return View(await GetContactViewModelAsync(command.Id));
            }

            return RedirectToAction("Index", new {contactId = command.ContactId});
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = await GetContactViewModelAsync(null);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm(Name = "Order")] AddOrderCommand command)
        {
            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateViewModelValidation(result.Errors, "Order");
                return View(await GetContactViewModelAsync(null));
            }

            return RedirectToAction("Index", new {contactId = command.ContactId});
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id, Guid contact)
        {
            var command = new DeleteOrderCommand(id);

            var result = await _mediator.CommandAsync(command);

            return RedirectToAction("Index", new {contactId = contact});
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
            return RedirectToAction("Index", new {contactId = id});
        }
    }
}