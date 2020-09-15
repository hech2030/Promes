using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyTeam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HichemsTestController : ControllerBase
    {
        private readonly ILogger<Hichem> _logger;

        public HichemsTestController(ILogger<Hichem> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("get")]
        public IEnumerable<Hichem> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Hichem
            {
                birthDate = DateTime.Now.AddDays(index),
                name = "hichem",
                age = 24
            })
            .ToArray();
        }

        [HttpGet]
        [Route("AnotherGet")]
        public IEnumerable<Hichem> AnotherGet()
        {
            return Enumerable.Range(1, 5).Select(index => new Hichem
            {
                birthDate = DateTime.Now.AddDays(index),
                name = "Another hichem",
                age = 25
            })
            .ToArray();
        }
    }
}
