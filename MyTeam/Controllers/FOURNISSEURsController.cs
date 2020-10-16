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
    public class FOURNISSEURsController : ControllerBase
    {
        public FOURNISSEURsController()
        {
        }

        [HttpPost]
        [Route("FindFOURNISSEUR")]
        public IActionResult FindFOURNISSEUR(FOURNISSEURFindRequest request)
        {
            IEnumerable<FOURNISSEUR> data = new List<FOURNISSEUR>();
            data = FournisseurDatabaseBusinessProvider.Instance.Find(request.id);
            if (request.nomFournisseur != null)
            {
                data = data.Where(x => x.NomF == request.nomFournisseur);
            }
            return Ok(new { result = data });
        }

        [HttpPost]
        public IActionResult Post([FromBody] FOURNISSEUR obj)
        {
            var data = FournisseurDatabaseBusinessProvider.Instance.Add(obj);
            return Ok(new { success = true });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FOURNISSEUR obj)
        {
            var data = FournisseurDatabaseBusinessProvider.Instance.Update(id, obj);
            return Ok(new { success = true });
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool success = false;
            try
            {
                FournisseurDatabaseBusinessProvider.Instance.Remove(id);
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

