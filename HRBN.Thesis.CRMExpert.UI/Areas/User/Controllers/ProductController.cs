using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application;
using HRBN.Thesis.CRMExpert.Application.Core.Mediator;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Product;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Product;
using HRBN.Thesis.CRMExpert.UI.Filters;
using HRBN.Thesis.CRMExpert.UI.ViewModels.User.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.User.Controllers
{
    [Area("User")]
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private Guid GetCurrentUserId()
        {
            return Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                .Value);
        }

        public async Task<IActionResult> Index(SearchProductsQuery query)
        {
            var queryToBeProcessed = query;
            if (queryToBeProcessed.PageNumber <= 0 || queryToBeProcessed.PageSize <= 0)
            {
                queryToBeProcessed = new SearchProductsQuery()
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

        public async Task<ProductViewModel> GetProductViewModelAsync(Guid? productId)
        {
            GetProductQuery query = null;
            ProductDto result = null;

            if (productId.HasValue)
            {
                query = new GetProductQuery(productId.Value);
                result = await _mediator.QueryAsync(query);
            }
            else
            {
                result = new ProductDto();
            }

            return new ProductViewModel(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await GetProductViewModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm(Name = "Product")] EditProductCommand command)
        {
            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateViewModelValidation(result.Errors, "Product");
                return View(await GetProductViewModelAsync(command.Id));
            }

            return Redirect("/User/Product");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = await GetProductViewModelAsync(null);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm(Name = "Product")] AddProductCommand command)
        {
            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateViewModelValidation(result.Errors, "Product");
                return View(await GetProductViewModelAsync(null));
            }

            return Redirect("/User/Product");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProductCommand(id);

            var result = await _mediator.CommandAsync(command);

            return Redirect("/User/Product");
        }
    }
}