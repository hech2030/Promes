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
    public class ENTREEsController : ControllerBase
    {
        public ENTREEsController()
        {
        }

        [HttpPost]
        [Route("FindENTREE")]
        public IActionResult findENTREE(EntreeFindRequest request)
        {
            IEnumerable<ENTREE> data = new List<ENTREE>();
            data = EntreeDatabaseBusinessProvider.Instance.Find(request.id);
            if (request.numEntree > 0)
            {
                data = data.Where(x => x.numEntree == request.numEntree);
            }
            if (request.ARTICLEId > 0)
            {
                data = data.Where(x => x.ARTICLEId == request.ARTICLEId);
            }
            return Ok(new { result = data });
        }

        [HttpPost]
        public IActionResult Post([FromBody] ENTREE obj)
        {
            var data = EntreeDatabaseBusinessProvider.Instance.Add(obj);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ENTREE obj)
        {
            var data = EntreeDatabaseBusinessProvider.Instance.Update(id, obj);
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            EntreeDatabaseBusinessProvider.Instance.Remove(id);
            return Ok();

        }
    }
}

