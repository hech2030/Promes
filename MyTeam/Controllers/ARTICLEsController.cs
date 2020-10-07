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
    public class ARTICLEsController : ControllerBase
    {
        readonly SolarThermalEntities ArtDetails;
        public ARTICLEsController(SolarThermalEntities ARTICLEContext)
        {
            ArtDetails = ARTICLEContext;
        }

        [HttpGet]
        public IEnumerable<ARTICLE> Get()
        {
            var data = ArtDetails.ARTICLE.ToList();
            return data;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ARTICLE obj)
        {
            var data = ArtDetails.ARTICLE.Add(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ARTICLE obj)
        {
            var data = ArtDetails.ARTICLE.Update(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = ArtDetails.ARTICLE.Where(a => a.Id == id).FirstOrDefault();
            ArtDetails.ARTICLE.Remove(data);
            ArtDetails.SaveChanges();
            return Ok();

        }
    }
}

