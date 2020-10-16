using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAcess;
using Microsoft.AspNetCore.Mvc;
using MyTeam.Common.Requests.bo.Users;

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

        [HttpPost]
        [Route("FindCATEGORIE_ART")]
        public IActionResult findCATEGORIE_ART(CATEGORIE_ARTFindRequest request)
        {
            IEnumerable<CATEGORIE_ART> data = new List<CATEGORIE_ART>();
            if (request.id > 0)
            {
                data = (from a in ArtDetails.CATEGORIE_ART
                        select a).Where(a => a.Id == request.id).ToList();
            }
            else
            {
                data = (from a in ArtDetails.CATEGORIE_ART
                        select a).ToList();
            }
            if (request.nomCategorie != null)
            {
                data = data.Where(x => x.nomCate == request.nomCategorie);
            }
            return Ok(new { result = data });
        }

        [HttpPost]
        public IActionResult Post([FromBody] CATEGORIE_ART obj)
        {
            var data = ArtDetails.CATEGORIE_ART.Add(obj);
            ArtDetails.SaveChanges();
            return Ok(new { success = true });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CATEGORIE_ART obj)
        {
            var data = ArtDetails.CATEGORIE_ART.Update(obj);
            ArtDetails.SaveChanges();
            return Ok(new { success = true });
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool success = false;
            try
            {
                var data = ArtDetails.CATEGORIE_ART.Where(a => a.Id == id).FirstOrDefault();
                ArtDetails.CATEGORIE_ART.Remove(data);
                ArtDetails.SaveChanges();
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { success });
            }
        }
    }
}

