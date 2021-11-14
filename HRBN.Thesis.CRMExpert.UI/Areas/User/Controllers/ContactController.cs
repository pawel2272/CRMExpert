using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.User.Controllers
{
    [Area("User")]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}