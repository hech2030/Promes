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
    public class COMMANDEsController : ControllerBase
    {
        public COMMANDEsController()
        {
        }

        [HttpGet]
        public IEnumerable<COMMANDE> Get()
        {
            var data = CommandeDatabaseBusinessProvider.Instance.Get();
            return data;
        }

        [HttpPost]
        public IActionResult Post([FromBody] COMMANDE obj)
        {
            var data = CommandeDatabaseBusinessProvider.Instance.Add(obj);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] COMMANDE obj)
        {
            var data = CommandeDatabaseBusinessProvider.Instance.Update(id, obj);
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            CommandeDatabaseBusinessProvider.Instance.Remove(id);
            return Ok();

        }
    }
}

