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
    public class LIGNE_COMMANDEsController : ControllerBase
    {
        public LIGNE_COMMANDEsController()
        {
        }

        [HttpGet]
        public IEnumerable<LIGNE_COMMANDE> Get()
        {
            var data = LigneCommandeDatabaseBusinessProvider.Instance.Get();
            return data;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LIGNE_COMMANDE obj)
        {
            var data = LigneCommandeDatabaseBusinessProvider.Instance.Add(obj);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] LIGNE_COMMANDE obj)
        {
            var data = LigneCommandeDatabaseBusinessProvider.Instance.Update(id, obj);
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            LigneCommandeDatabaseBusinessProvider.Instance.Remove(id);
            return Ok();

        }
    }
}

