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
    public class RECEPTIONsController : ControllerBase
    {
        readonly SolarThermalEntities ArtDetails;
        public RECEPTIONsController(SolarThermalEntities RECEPTIONContext)
        {
            ArtDetails = RECEPTIONContext;
        }

        [HttpGet]
        public IEnumerable<RECEPTION> Get()
        {
            var data = ArtDetails.RECEPTION.ToList();
            return data;
        }

        [HttpPost]
        public IActionResult Post([FromBody] RECEPTION obj)
        {
            var data = ArtDetails.RECEPTION.Add(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RECEPTION obj)
        {
            var data = ArtDetails.RECEPTION.Update(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = ArtDetails.RECEPTION.Where(a => a.Id == id).FirstOrDefault();
            ArtDetails.RECEPTION.Remove(data);
            ArtDetails.SaveChanges();
            return Ok();

        }
    }
}

