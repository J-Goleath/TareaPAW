using PAW.Architecture.Providers;
using System.Text.Json.Serialization;

namespace PAW.Web.Services;

public interface IExchangeRateService
{
    Task<TipoCambioResponse> GetExchangeRateAsync(CancellationToken ct = default);
}

/// <summary>
/// Servicio del lado del SERVIDOR para el tipo de cambio.
/// En la Tarea 2 la vista ya NO lo usa: el tipo de cambio lo pide JavaScript.
/// Solo se usa como "plan B" desde ExchangeRateController (/ExchangeRate/Get)
/// por si el navegador bloquea la llamada directa a Hacienda (CORS).
/// </summary>
public class ExchangeRateService : IExchangeRateService
{
    private readonly TipoCambioClient _client;

    // El TipoCambioClient llega por inyección de dependencias (ver Program.cs).
    public ExchangeRateService(TipoCambioClient client)
    {
        _client = client;
    }

    public Task<TipoCambioResponse> GetExchangeRateAsync(CancellationToken ct = default)
        => _client.GetTipoCambioAsync(ct);
}

// DTO Data transfer Objects for the API response
public class TipoCambioResponse
{
    [JsonPropertyName("dolar")]
    public Dolar Dolar { get; set; } = new();

    [JsonPropertyName("euro")]
    public Euro Euro { get; set; } = new();
}

public class Dolar
{
    [JsonPropertyName("venta")]
    public ValorFecha Venta { get; set; } = new();

    [JsonPropertyName("compra")]
    public ValorFecha Compra { get; set; } = new();
}

public class ValorFecha
{
    [JsonPropertyName("fecha")]
    public DateTime Fecha { get; set; }

    [JsonPropertyName("valor")]
    public decimal Valor { get; set; }
}

public class Euro
{
    [JsonPropertyName("fecha")]
    public DateTime Fecha { get; set; }

    [JsonPropertyName("dolares")]
    public decimal Dolares { get; set; }

    [JsonPropertyName("colones")]
    public decimal Colones { get; set; }
}

/// <summary>
/// Cliente HTTP que llama a la API de Hacienda.
/// El HttpClient lo crea y configura Program.cs (AddHttpClient).
/// </summary>
public class TipoCambioClient
{
    private const string Path = "indicadores/tc";
    private readonly HttpClient _http;

    public TipoCambioClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<TipoCambioResponse> GetTipoCambioAsync(CancellationToken ct = default)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, Path)
        {
            Version = System.Net.HttpVersion.Version11,
            VersionPolicy = HttpVersionPolicy.RequestVersionExact
        };

        var res = await _http.SendAsync(req, ct);
        res.EnsureSuccessStatusCode();
        var content = await res.Content.ReadAsStringAsync(ct);

        return await JsonProvider.DeserializeAsync<TipoCambioResponse>(content)
            ?? throw new InvalidOperationException("Empty response from tipo de cambio API.");
    }
}
