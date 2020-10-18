using System;
using System.Collections.Generic;
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
    public class SORTIEsController : ControllerBase
    {
        public SORTIEsController()
        {
        }

        [HttpPost]
        [Route("FindSORTIE")]
        public IActionResult findSORTIE(SortieFindRequest request)
        {
            IEnumerable<SORTIE> data = new List<SORTIE>();
            data = SortieDatabaseBusinessProvider.Instance.Find(request.id);
            if (request.numSortie > 0)
            {
                data = data.Where(x => x.numSortie == request.numSortie);
            }
            if (request.ARTICLEId > 0)
            {
                data = data.Where(x => x.ARTICLEId == request.ARTICLEId);
            }
            return Ok(new { result = data });
        }

        [HttpPost]
        public IActionResult Post([FromBody] SORTIE obj)
        {
            var data = SortieDatabaseBusinessProvider.Instance.Add(obj);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SORTIE obj)
        {
            var data = SortieDatabaseBusinessProvider.Instance.Update(id, obj);
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SortieDatabaseBusinessProvider.Instance.Remove(id);
            return Ok();

        }
    }
}

