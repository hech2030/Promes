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
    public class SORTIEsController : ControllerBase
    {
        public SORTIEsController()
        {
        }

        [HttpGet]
        public IEnumerable<SORTIE> Get()
        {
            var data = SortieDatabaseBusinessProvider.Instance.Get();
            return data;
        }

        [HttpPost]
        public IActionResult Post([FromBody] SORTIE obj)
        {
            var data = SortieDatabaseBusinessProvider.Instance.Add(obj);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SORTIE obj)
        {
            var data = SortieDatabaseBusinessProvider.Instance.Update(id, obj);
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SortieDatabaseBusinessProvider.Instance.Remove(id);
            return Ok();

        }
    }
}

