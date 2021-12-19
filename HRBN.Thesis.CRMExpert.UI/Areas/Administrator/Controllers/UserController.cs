using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.Administrator.Controllers;

[Area("Administrator")]
public class UserController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}