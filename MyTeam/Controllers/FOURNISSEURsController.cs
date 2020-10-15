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
    public class FOURNISSEURsController : ControllerBase
    {
        readonly SolarThermalEntities ArtDetails;
        public FOURNISSEURsController(SolarThermalEntities FOURNISSEURContext)
        {
            ArtDetails = FOURNISSEURContext;
        }

        [HttpPost]
        [Route("FindFOURNISSEUR")]
        public IEnumerable<FOURNISSEUR> findFOURNISSEUR(FOURNISSEURFindRequest request)
        {
            IEnumerable<FOURNISSEUR> data;
            if (request.id > 0)
            {
                 data = (from a in ArtDetails.FOURNISSEUR
                                .Include("ARTICLE")
                                .Include("COMMANDE")
                               select a).Where(a => a.Id == request.id);
            }
            else
            {
                 data = (from a in ArtDetails.FOURNISSEUR
                                .Include("ARTICLE")
                                .Include("COMMANDE")
                            select a);
            }
            return data.ToList();
        }

        [HttpPost]
        public IActionResult Post([FromBody] FOURNISSEUR obj)
        {
            var data = ArtDetails.FOURNISSEUR.Add(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FOURNISSEUR obj)
        {
            var data = ArtDetails.FOURNISSEUR.Update(obj);
            ArtDetails.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = ArtDetails.FOURNISSEUR.Where(a => a.Id == id).FirstOrDefault();
            ArtDetails.FOURNISSEUR.Remove(data);
            ArtDetails.SaveChanges();
            return Ok();

        }
    }
}

