using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Application;
using HRBN.Thesis.CRMExpert.Application.Core.Mediator;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.User;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.User;
using HRBN.Thesis.CRMExpert.UI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.Identity.Controllers
{
    [Area("Identity")]
    [ServiceFilter(typeof(JwtAuthFilter))]
    public class AccountController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        private Guid GetCurrentUserId()
        {
            return Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                .Value);
        }
        
        [HttpGet]
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
        
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var currentUserId = GetCurrentUserId();

            var query = new GetUserQuery(currentUserId);

            var result = await _mediator.QueryAsync(query);
            
            return View(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View(new ChangePasswordCommand());
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            command.Id = GetCurrentUserId();
            
            var result = await _mediator.CommandAsync(command);
            
            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View();
            }

            return Redirect("/Identity/Account/Manage");
        }
        
        [HttpGet]
        public async Task<IActionResult> ModifyAddress()
        {
            var query = new GetUserQuery(GetCurrentUserId());

            var result = await _mediator.QueryAsync(query);
            
            return View(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> ModifyAddress(ChangeAddressCommand command)
        {
            command.Id = GetCurrentUserId();
            
            var result = await _mediator.CommandAsync(command);
            
            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View();
            }

            return Redirect("/Identity/Account/Manage");
        }
    }
}