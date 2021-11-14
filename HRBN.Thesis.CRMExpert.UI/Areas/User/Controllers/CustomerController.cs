using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.User.Controllers
{
    public class CustomerController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}