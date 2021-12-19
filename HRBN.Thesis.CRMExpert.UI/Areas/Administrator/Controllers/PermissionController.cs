using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.Administrator.Controllers;

[Area("Administrator")]
public class PermissionController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}