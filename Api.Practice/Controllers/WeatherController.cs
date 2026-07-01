namespace Api.Practice.Controllers;

using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Practice.Dtos;
using Api.Practice.Services;
using Api.Practice.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
    private readonly IValidation<ForecastDto> forecastDateValidation;
    private readonly IForecastService forecastService;

    public WeatherController(IValidation<ForecastDto> forecastDateValidation, IForecastService forecastService)
    {
        this.forecastDateValidation = forecastDateValidation ?? throw new ArgumentNullException(nameof(forecastDateValidation));
        this.forecastService = forecastService ?? throw new ArgumentNullException(nameof(forecastService));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ForecastDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetForecast([FromQuery] ForecastDto request, CancellationToken cancellationToken)
    {

        var validationResult = await this.forecastDateValidation.IsValid(request);

        if(!validationResult)
            return BadRequest();

        var forecast = await this.forecastService.GetForecast(request.PostalCode, request.Time);

        var response = new ForecastDto() { PostalCode = forecast.PostalCode, Time = forecast.Time, Temperature = forecast.Temperature, Weather = forecast.Weather };
        return Ok(response);
    }
}