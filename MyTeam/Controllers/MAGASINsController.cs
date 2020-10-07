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
    public class MAGASINsController : ControllerBase
    {
        readonly SolarThermalEntities ArtDetails;
        public MAGASINsController(SolarThermalEntities MAGASINContext)
        {
            ArtDetails = MAGASINContext;
        }

        [HttpGet]
        public IEnumerable<MAGASIN> Get()
        {
            var data = ArtDetails.MAGASIN.ToList();
            return data;
        }

        [HttpPost]
        public IActionResult Post([FromBody] MAGASIN obj)
        {
            var data = ArtDetails.MAGASIN.Add(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MAGASIN obj)
        {
            var data = ArtDetails.MAGASIN.Update(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = ArtDetails.MAGASIN.Where(a => a.Id == id).FirstOrDefault();
            ArtDetails.MAGASIN.Remove(data);
            ArtDetails.SaveChanges();
            return Ok();

        }
    }
}

