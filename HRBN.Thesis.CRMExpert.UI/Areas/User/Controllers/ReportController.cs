using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.User.Controllers;

[Area("User")]
public class ReportController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}