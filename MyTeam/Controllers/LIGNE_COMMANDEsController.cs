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
    public class LIGNE_COMMANDEsController : ControllerBase
    {
        readonly SolarThermalEntities ArtDetails;
        public LIGNE_COMMANDEsController(SolarThermalEntities LIGNE_COMMANDEContext)
        {
            ArtDetails = LIGNE_COMMANDEContext;
        }

        [HttpGet]
        public IEnumerable<LIGNE_COMMANDE> Get()
        {
            var data = ArtDetails.LIGNE_COMMANDE.ToList();
            return data;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LIGNE_COMMANDE obj)
        {
            var data = ArtDetails.LIGNE_COMMANDE.Add(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] LIGNE_COMMANDE obj)
        {
            var data = ArtDetails.LIGNE_COMMANDE.Update(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = ArtDetails.LIGNE_COMMANDE.Where(a => a.Id == id).FirstOrDefault();
            ArtDetails.LIGNE_COMMANDE.Remove(data);
            ArtDetails.SaveChanges();
            return Ok();

        }
    }
}

