using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        // GET
        public IActionResult Login()
        {
            return View();
        }
    }
}