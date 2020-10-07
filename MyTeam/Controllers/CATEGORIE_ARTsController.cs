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
    public class CATEGORIE_ARTsController : ControllerBase
    {
        readonly SolarThermalEntities ArtDetails;
        public CATEGORIE_ARTsController(SolarThermalEntities CATEGORIE_ARTContext)
        {
            ArtDetails = CATEGORIE_ARTContext;
        }

        [HttpGet]
        public IEnumerable<CATEGORIE_ART> Get()
        {
            var data = ArtDetails.CATEGORIE_ART.ToList();
            return data;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CATEGORIE_ART obj)
        {
            var data = ArtDetails.CATEGORIE_ART.Add(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CATEGORIE_ART obj)
        {
            var data = ArtDetails.CATEGORIE_ART.Update(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = ArtDetails.CATEGORIE_ART.Where(a => a.Id == id).FirstOrDefault();
            ArtDetails.CATEGORIE_ART.Remove(data);
            ArtDetails.SaveChanges();
            return Ok();

        }
    }
}

