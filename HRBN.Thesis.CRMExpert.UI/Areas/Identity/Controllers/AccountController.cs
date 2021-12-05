using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Microsoft.AspNetCore.Mvc.Controller
    {
        // GET
        public IActionResult Login()
        {
            return View();
        }
    }
}