using HRBN.Thesis.CRMExpert.UI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.User.Controllers;

[Area("User")]
[ServiceFilter(typeof(JwtAuthFilter))]
[Authorize]
public class ReportController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    public FileResult Download()
    {
        byte[] fileBytes = System.IO.File.ReadAllBytes(@"c:\folder\myfile.ext");
        string fileName = "myfile.ext";
        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
    }
}