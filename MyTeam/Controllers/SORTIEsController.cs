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
    public class SORTIEsController : ControllerBase
    {
        readonly SolarThermalEntities ArtDetails;
        public SORTIEsController(SolarThermalEntities SORTIEContext)
        {
            ArtDetails = SORTIEContext;
        }

        [HttpGet]
        public IEnumerable<SORTIE> Get()
        {
            var data = ArtDetails.SORTIE.ToList();
            return data;
        }

        [HttpPost]
        public IActionResult Post([FromBody] SORTIE obj)
        {
            var data = ArtDetails.SORTIE.Add(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SORTIE obj)
        {
            var data = ArtDetails.SORTIE.Update(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = ArtDetails.SORTIE.Where(a => a.Id == id).FirstOrDefault();
            ArtDetails.SORTIE.Remove(data);
            ArtDetails.SaveChanges();
            return Ok();

        }
    }
}

