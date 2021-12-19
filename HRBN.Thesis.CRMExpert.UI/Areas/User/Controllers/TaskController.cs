using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.User.Controllers;

[Area("User")]
public class TaskController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}