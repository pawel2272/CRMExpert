using HRBN.Thesis.CRMExpert.UI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.User.Controllers;

[Area("User")]
[ServiceFilter(typeof(JwtAuthFilter))]
[Authorize]
public class TaskController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}