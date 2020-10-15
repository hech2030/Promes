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
    public class ARTICLEsController : ControllerBase
    {
        readonly SolarThermalEntities ArtDetails;
        public ARTICLEsController(SolarThermalEntities ARTICLEContext)
        {
            ArtDetails = ARTICLEContext;
        }

        [HttpPost]
        [Route("FindArticle")]
        public IEnumerable<ARTICLE> findArticle(ArticleFindRequest request)
        {
            IEnumerable<ARTICLE> data;
            if (request.id > 0)
            {
                data = (from a in ArtDetails.ARTICLE
                                .Include("CATEGORIE_ART")
                                .Include("FOURNISSEUR")
                                .Include("MAGASIN")
                               select a).Where(a => a.Id == request.id).ToList();
            }
            else
            {
                 data = (from a in ArtDetails.ARTICLE
                                .Include("CATEGORIE_ART")
                                .Include("FOURNISSEUR")
                                .Include("MAGASIN")
                            select a);
            }
            return data.ToList();
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

