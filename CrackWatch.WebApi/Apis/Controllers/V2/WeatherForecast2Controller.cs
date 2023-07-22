using Asp.Versioning;
using CrackWatch.WebApi.Apis.Controllers.V1;
using Microsoft.AspNetCore.Mvc;

namespace CrackWatch.WebApi.Apis.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class WeatherForecast2Controller : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecast2Controller(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    
    
    /// <summary>
    /// This API returns.
    /// </summary>
    /// <remarks>
    /// Just for demonstration
    ///
    ///     GET api/v1/WeatherForecast
    ///     {
    ///     }
    ///     curl -X GET "https://server-url/api/v1/WeatherForecast" -H  "accept: text/plain"
    /// 
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">Return list of weather forecast </response>
    [HttpGet(Name = "GetWeatherForecast")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}