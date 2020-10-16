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
    public class MAGASINsController : ControllerBase
    {
        public MAGASINsController()
        {
        }

        [HttpPost]
        [Route("FindMAGASIN")]
        public IActionResult findMAGASIN(MAGASINFindRequest request)
        {
            IEnumerable<MAGASIN> data = new List<MAGASIN>();
            data = MagasinDatabaseBusinessProvider.Instance.Find(request.id);
            if (request.nomMagasin != null)
            {
                data = data.Where(x => x.nomMagasin == request.nomMagasin);
            }
            return Ok(new { result = data });
        }

        [HttpPost]
        public IActionResult Post([FromBody] MAGASIN obj)
        {
            var data = MagasinDatabaseBusinessProvider.Instance.Add(obj);
            return Ok(new { success = true });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MAGASIN obj)
        {
            var data = MagasinDatabaseBusinessProvider.Instance.Update(id, obj);
            return Ok(new { success = true });
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool success = false;
            try
            {
                MagasinDatabaseBusinessProvider.Instance.Remove(id);
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

