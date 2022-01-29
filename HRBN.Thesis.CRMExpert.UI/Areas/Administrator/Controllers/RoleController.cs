using HRBN.Thesis.CRMExpert.Application.Core.Mediator;
using HRBN.Thesis.CRMExpert.UI.Filters;
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
    
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Edit()
    {
        return View();
    }
}