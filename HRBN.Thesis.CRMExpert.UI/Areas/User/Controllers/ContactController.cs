using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application.Core.Mediator;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Contact;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.User.Controllers
{
    [Area("User")]
    public class ContactController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(SearchContactsQuery? query)
        {
            if (query.UserId.Equals(Guid.Empty))
            {
                var userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                    .Value);
                query.UserId = userId;
            }

            ViewBag.PageNumber = query.PageNumber;

            var contacts = await _mediator.QueryAsync(query);
            return View(contacts);
            var results = await _mediator.QueryAsync(new SearchContactsQuery()
            {
                UserId = Guid.Empty,
                SearchPhrase = null,
                PageNumber = 1,
                PageSize = 10,
                OrderBy = "FirstName",
                SortDirection = SortDirection.DESC
            });
            return View(results);
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}