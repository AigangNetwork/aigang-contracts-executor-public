using System;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Aigang.Contracts.Executor.API.Controllers
{
    [Route("api/status")]
    public class StatusController : ControllerBase
    {
        private static string EnvironmentName { get; set; }
        private static readonly ILog _logger = LogManager.GetLogger(typeof(StatusController));
        
        public StatusController(IHostingEnvironment env)
        {
            EnvironmentName = env.EnvironmentName;
        }
        /// <summary>
        /// PING Endpoint
        /// </summary>
        [HttpGet("ping")]
        public string Ping()
        {
            return "PONG";
        }

        /// <summary>
        /// PING Endpoint
        /// </summary>
        [HttpGet]
        public IActionResult Status()
        {
            var result = new
            {
                Status = "OK",
                Date = DateTime.UtcNow,
                Environment = EnvironmentName
            };

            return Ok(result);
        }
    }
}