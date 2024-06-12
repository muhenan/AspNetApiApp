using Microsoft.AspNetCore.Mvc;

namespace AspNetApiApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    /*
    The example you provided already uses some functional programming concepts, 
    such as using Enumerable.Range and Select for declarative data processing.
    */
}


/*

Here’s an example of the Get method in a more functional programming style:

public IEnumerable<WeatherForecast> Get()
{
    var now = DateTime.Now;
    return Enumerable.Range(1, 5)
        .Select(index => CreateWeatherForecast(now, index))
        .ToArray();
}

private WeatherForecast CreateWeatherForecast(DateTime now, int index)
{
    return new WeatherForecast
    {
        Date = now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    };
}

Breakdown of Functional Concepts

1. Immutability:
now is a local immutable variable that holds the current date and time.
WeatherForecast objects are created without modifying any external state.

2. Pure Functions:
CreateWeatherForecast is a pure function. It takes input parameters and returns a new WeatherForecast object without side effects.

3. First-Class and Higher-Order Functions:
Select is a higher-order function that takes a function (CreateWeatherForecast) as an argument.

4. Declarative Code:
The use of Enumerable.Range and Select focuses on what to do (generate and transform a range of numbers) rather than how to do it procedurally.

*/
