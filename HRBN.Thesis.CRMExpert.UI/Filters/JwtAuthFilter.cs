using System;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HRBN.Thesis.CRMExpert.UI.Filters;

public class JwtAuthFilter : IAsyncActionFilter
{
    private readonly IUnitOfWork _unitOfWork;

    public JwtAuthFilter(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private CookieBuilder CreateAuthorizationCookie(double time)
    {
        CookieBuilder cookie = new CookieBuilder()
        {
            IsEssential = true,
            Expiration = TimeSpan.FromMinutes(time),
            Name = "Authorization"
        };
        return cookie;
    }
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await next();
        
        bool isAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
        bool isTokenActive = await _unitOfWork.TokenRepository.IsCurrentActiveToken();
        bool isLoginPage = context.HttpContext.Request.Path.Equals("/Identity/Account/Login");

        if (/*!isAuthenticated &&*/ !isTokenActive && !isLoginPage)
        {
            context.HttpContext.Response.Cookies.Delete("Authorization");
            CookieBuilder cookie = CreateAuthorizationCookie(-60);
            context.HttpContext.Response.Cookies.Append("Authorization", "", cookie.Build(context.HttpContext));
            context.HttpContext.Response.Redirect("/Identity/Account/Login");
        }
        
        if(/*isAuthenticated &&*/ isTokenActive && isLoginPage)
        {
            context.HttpContext.Response.Redirect("/User/Home");
        }

    }
}