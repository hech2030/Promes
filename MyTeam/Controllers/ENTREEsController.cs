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
    public class ENTREEsController : ControllerBase
    {
        readonly SolarThermalEntities ArtDetails;
        public ENTREEsController(SolarThermalEntities ENTREEContext)
        {
            ArtDetails = ENTREEContext;
        }

        [HttpGet]
        public IEnumerable<ENTREE> Get()
        {
            var data = ArtDetails.ENTREE.ToList();
            return data;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ENTREE obj)
        {
            var data = ArtDetails.ENTREE.Add(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ENTREE obj)
        {
            var data = ArtDetails.ENTREE.Update(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = ArtDetails.ENTREE.Where(a => a.Id == id).FirstOrDefault();
            ArtDetails.ENTREE.Remove(data);
            ArtDetails.SaveChanges();
            return Ok();

        }
    }
}

