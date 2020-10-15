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
    public class MAGASINsController : ControllerBase
    {
        readonly SolarThermalEntities ArtDetails;
        public MAGASINsController(SolarThermalEntities MAGASINContext)
        {
            ArtDetails = MAGASINContext;
        }

        [HttpPost]
        [Route("FindMAGASIN")]
        public IEnumerable<MAGASIN> findMAGASIN(MAGASINFindRequest request)
        {
            IEnumerable<MAGASIN> data;
            if (request.id > 0)
            {
                 data = (from a in ArtDetails.MAGASIN
                                .Include("ARTICLE")
                               select a).Where(a => a.Id == request.id);
            }
            else
            {
                 data = (from a in ArtDetails.MAGASIN
                                .Include("ARTICLE")
                            select a);
            }
            return data.ToList();
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

