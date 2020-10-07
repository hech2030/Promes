using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAcess;
using Microsoft.AspNetCore.Mvc;


namespace MyTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class COMMANDEsController : ControllerBase
    {
        readonly SolarThermalEntities ArtDetails;
        public COMMANDEsController(SolarThermalEntities COMMANDEContext)
        {
            ArtDetails = COMMANDEContext;
        }

        [HttpGet]
        public IEnumerable<COMMANDE> Get()
        {
            var data = ArtDetails.COMMANDE.ToList();
            return data;
        }

        [HttpPost]
        public IActionResult Post([FromBody] COMMANDE obj)
        {
            var data = ArtDetails.COMMANDE.Add(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] COMMANDE obj)
        {
            var data = ArtDetails.COMMANDE.Update(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = ArtDetails.COMMANDE.Where(a => a.Id == id).FirstOrDefault();
            ArtDetails.COMMANDE.Remove(data);
            ArtDetails.SaveChanges();
            return Ok();

        }
    }
}

