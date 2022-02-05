using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CsvHelper;
using HRBN.Thesis.CRMExpert.Application.Core.Mediator;
using HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Queries.Report;
using HRBN.Thesis.CRMExpert.UI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRBN.Thesis.CRMExpert.UI.Areas.User.Controllers;

[Area("User")]
[ServiceFilter(typeof(JwtAuthFilter))]
[Authorize]
public class ReportController : Controller
{
    private readonly IMediator _mediator;

    public ReportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private Guid GetCurrentUserId()
    {
        return Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
            .Value);
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<FileResult> LastMonthOrders()
    {
        var result = await _mediator.QueryAsync(new GetLastMonthOrdersReportQuery());
        byte[] fileBytes = null;

        await using (var memoryStream = new MemoryStream())
        await using (var streamWriter = new StreamWriter(memoryStream))
        await using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
        {
            await csvWriter.WriteRecordsAsync(result);
            await streamWriter.FlushAsync();
            fileBytes = memoryStream.ToArray();
        }

        string fileName = "LastMonthOrders.csv";

        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
    }

    [HttpGet]
    public async Task<FileResult> ListOfMyContacts()
    {
        var result = await _mediator.QueryAsync(new GetListOfMyContactsQuery(GetCurrentUserId()));
        byte[] fileBytes = null;

        await using (var memoryStream = new MemoryStream())
        await using (var streamWriter = new StreamWriter(memoryStream))
        await using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
        {
            await csvWriter.WriteRecordsAsync(result);
            await streamWriter.FlushAsync();
            fileBytes = memoryStream.ToArray();
        }

        string fileName = "ListOfMyContacts.csv";

        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
    }

    [HttpGet]
    public async Task<FileResult> OftenOrderedProducts()
    {
        var result = await _mediator.QueryAsync(new GetOftenOrderedProductsReportQuery());
        byte[] fileBytes = null;

        await using (var memoryStream = new MemoryStream())
        await using (var streamWriter = new StreamWriter(memoryStream))
        await using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
        {
            await csvWriter.WriteRecordsAsync(result);
            await streamWriter.FlushAsync();
            fileBytes = memoryStream.ToArray();
        }

        string fileName = "OftenOrderedProducts.csv";

        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
    }

    [HttpGet]
    public async Task<FileResult> YesterdayOrders()
    {
        var result = await _mediator.QueryAsync(new GetYesterdayOrdersQuery());
        byte[] fileBytes = null;

        await using (var memoryStream = new MemoryStream())
        await using (var streamWriter = new StreamWriter(memoryStream))
        await using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
        {
            await csvWriter.WriteRecordsAsync(result);
            await streamWriter.FlushAsync();
            fileBytes = memoryStream.ToArray();
        }

        string fileName = "YesterdayOrders.csv";

        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
    }
}