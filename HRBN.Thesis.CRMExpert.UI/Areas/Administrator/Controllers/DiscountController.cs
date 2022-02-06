using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application;
using HRBN.Thesis.CRMExpert.Application.Core.Mediator;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Discount;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Customer;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Discount;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Product;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.User;
using HRBN.Thesis.CRMExpert.UI.Filters;
using HRBN.Thesis.CRMExpert.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.Administrator.Controllers;

[Area("Administrator")]
[ServiceFilter(typeof(JwtAuthFilter))]
[Authorize(Roles = "Admin")]
public class DiscountController : Controller
{
    private readonly IMediator _mediator;

    public DiscountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private Guid GetCurrentUserId()
    {
        return Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
            .Value);
    }

    public async Task<IActionResult> Index(SearchDiscountsQuery query)
    {
        var queryToBeProcessed = query;
        if (queryToBeProcessed.PageNumber <= 0 || queryToBeProcessed.PageSize <= 0)
        {
            queryToBeProcessed = new SearchDiscountsQuery()
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

    public async Task<DiscountViewModel> GetDiscountViewModelAsync(Guid? discountId)
    {
        GetDiscountQuery query = null;
        DiscountDto result = null;

        if (discountId.HasValue)
        {
            query = new GetDiscountQuery(discountId.Value);
            result = await _mediator.QueryAsync(query);
        }
        else
        {
            result = new DiscountDto();
        }

        var productData = await _mediator.QueryAsync(new GetProductDataQuery());
        var customerData = await _mediator.QueryAsync(new GetCustomerDataQuery());

        return new DiscountViewModel(result, productData, customerData);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var model = await GetDiscountViewModelAsync(id);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromForm(Name = "Discount")] EditDiscountCommand command)
    {
        var result = await _mediator.CommandAsync(command);

        if (result.IsFailure)
        {
            ModelState.PopulateViewModelValidation(result.Errors, "Discount");
            return View(await GetDiscountViewModelAsync(command.Id));
        }

        return Redirect("/Administrator/Discount");
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = await GetDiscountViewModelAsync(null);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm(Name = "Discount")] AddDiscountCommand command)
    {
        var result = await _mediator.CommandAsync(command);

        if (result.IsFailure)
        {
            ModelState.PopulateViewModelValidation(result.Errors, "Discount");
            return View(await GetDiscountViewModelAsync(null));
        }

        return Redirect("/Administrator/Discount");
    }


    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteDiscountCommand(id);

        var result = await _mediator.CommandAsync(command);

        return Redirect("/Administrator/Discount");
    }
}