using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAcess.Business;
using DataAcess.Models;
using Microsoft.AspNetCore.Mvc;
using MyTeam.Common.Requests.bo.Users;

namespace MyTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CATEGORIE_ARTsController : ControllerBase
    {
        public object CategorieArtDatabaseBusinessProvider { get; private set; }

        public CATEGORIE_ARTsController()
        {
        }

        [HttpPost]
        [Route("FindCATEGORIE_ART")]
        public IActionResult FindCATEGORIE_ART(CATEGORIE_ARTFindRequest request)
        {
            IEnumerable<CATEGORIE_ART> data = new List<CATEGORIE_ART>();
            data = DataAcess.Business.CategorieArtDatabaseBusinessProvider.Instance.Find(request.id);
            if (request.nomCategorie != null)
            {
                data = data.Where(x => x.nomCate == request.nomCategorie);
            }
            return Ok(new { result = data });
        }

        [HttpPost]
        public IActionResult Post([FromBody] CATEGORIE_ART obj)
        {
            var data = DataAcess.Business.CategorieArtDatabaseBusinessProvider.Instance.Add(obj);
            return Ok(new { success = true });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CATEGORIE_ART obj)
        {
            var data = DataAcess.Business.CategorieArtDatabaseBusinessProvider.Instance.Update(id, obj);
            return Ok(new { success = true });
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool success = false;
            try
            {
                DataAcess.Business.CategorieArtDatabaseBusinessProvider.Instance.Remove(id);
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

