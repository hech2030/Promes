using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAcess.Business;
using DataAcess.Models;
using Microsoft.AspNetCore.Mvc;


namespace MyTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RECEPTIONsController : ControllerBase
    {
        public RECEPTIONsController()
        {
        }

        [HttpGet]
        public IEnumerable<RECEPTION> Get()
        {
            var data = ReceptionDatabaseBusinessProvider.Instance.Get();
            return data;
        }

        [HttpPost]
        public IActionResult Post([FromBody] RECEPTION obj)
        {
            var data = ReceptionDatabaseBusinessProvider.Instance.Add(obj);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RECEPTION obj)
        {
            var data = ReceptionDatabaseBusinessProvider.Instance.Update(id, obj);
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ReceptionDatabaseBusinessProvider.Instance.Remove(id);
            return Ok();

        }
    }
}

