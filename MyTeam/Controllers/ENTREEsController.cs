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
    public class ENTREEsController : ControllerBase
    {
        public ENTREEsController()
        {
        }

        [HttpGet]
        public IEnumerable<ENTREE> Get()
        {
            var data = EntreeDatabaseBusinessProvider.Instance.Get();
            return data;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ENTREE obj)
        {
            var data = EntreeDatabaseBusinessProvider.Instance.Add(obj);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ENTREE obj)
        {
            var data = EntreeDatabaseBusinessProvider.Instance.Update(id, obj);
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            EntreeDatabaseBusinessProvider.Instance.Remove(id);
            return Ok();

        }
    }
}

