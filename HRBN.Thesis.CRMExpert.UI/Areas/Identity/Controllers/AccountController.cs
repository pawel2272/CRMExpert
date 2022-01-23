using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application;
using HRBN.Thesis.CRMExpert.Application.Core.Mediator;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User;
using HRBN.Thesis.CRMExpert.UI.Areas.User.Controllers;
using HRBN.Thesis.CRMExpert.UI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [ServiceFilter(typeof(JwtAuthFilter))]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View();
            }

            return Redirect("/User/Home");
        }
        
        // GET
        public IActionResult Manage()
        {
            return View();
        }
    }
}