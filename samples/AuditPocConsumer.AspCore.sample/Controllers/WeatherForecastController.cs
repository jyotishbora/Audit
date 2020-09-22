using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Force.DeepCloner;
using IAS.Audit;
using IAS.Audit.Abstractions;
using IAS.Audit.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuditPocConsumer.AspCore.sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAuditManager _auditManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IAuditManager auditManager)
        {
            _logger = logger;
            _auditManager = auditManager;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var rng = new Random();

            var userId = 677623;
            //Simple user action Auditing
           await _auditManager.SaveAuditEvent(new AuditEvent
            {
                ApplicationName = "Weather Forecast API",
                EventName = "Weather API Access",
                TimeStamp = DateTime.Now,
                UserId = userId.ToString(),
                EntityId = 23,
                EventType = AuditEventType.UserAction,
                AuditText = $"User-{userId} accessed Weather API"
            });




            //Initial state
            var forecast = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            };

            //Auditing by creating a scope, AuditEvent will be saved once code block goes out of scope
            await using var auditScope = _auditManager.CreateScope(options =>
            {
                options.UserId = userId;
                options.EventName = "Weather API";
                options.EntityId = 117;
                options.TargetEntity = forecast;// target object to audit, we can make it an array 

            });
            
            //forecast has been mutated
            forecast.Date=DateTime.Now;
            forecast.TemperatureC = rng.Next(-20, 55);
            forecast.Summary = Summaries[rng.Next(Summaries.Length)];

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }


        [HttpGet]
        [Route("weather/today")]
        public async Task<WeatherForecast> GetCurrentWeather()
        {
            var rng = new Random();

            //Initial state
            var forecast = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            };

            var initialState = forecast.DeepClone();

            //forecast has been mutated
            forecast.Date = DateTime.Now;
            forecast.TemperatureC = rng.Next(-20, 55);
            forecast.Summary = Summaries[rng.Next(Summaries.Length)];

            //Create Entity Audit Event Manually 
            await _auditManager.SaveAuditEvent(new AuditEvent
            {
                ApplicationName = "Weather Forecast",
                EventName = "Weather API",
                TimeStamp = DateTime.Now,
                UserId = "11223",
                EntityId = 23,
                EventType = AuditEventType.EntityMutation,
                Target = new AuditEntity
                {
                    Name = "Getting Daily Forecast", 
                    InitialState = initialState,
                    FinalState = forecast
                }
            });

            return forecast;
        }
    }
}
