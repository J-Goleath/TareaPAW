using Microsoft.AspNetCore.Mvc;
using PAW.Web.Services;

namespace PAW.Web.Controllers;

/// <summary>
/// PLAN B del tipo de cambio.
/// Este controller NO alimenta ninguna vista. Solo devuelve el JSON de
/// Hacienda (JsonResult) para que site.js lo use si el navegador bloquea
/// la llamada directa a api.hacienda.go.cr por CORS.
/// URL: GET /ExchangeRate/Get
/// </summary>
public class ExchangeRateController : Controller
{
    private readonly IExchangeRateService _exchangeRateService;

    public ExchangeRateController(IExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        try
        {
            var tipoCambio = await _exchangeRateService.GetExchangeRateAsync(ct);
            return Json(tipoCambio);   // JsonResult
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status502BadGateway,
                new { message = "No se pudo obtener el tipo de cambio de Hacienda." });
        }
    }
}
