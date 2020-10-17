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
    public class ARTICLEsController : ControllerBase
    {
        public ARTICLEsController()
        {
        }

        [HttpPost]
        [Route("FindArticle")]
        public IActionResult FindArticle(ArticleFindRequest request)
        {
            IEnumerable<ARTICLE> data = new List<ARTICLE>();
            data = ArticleDatabaseBusinessProvider.Instance.Find(request.id);
            if (request.designation != null)
            {
                data = data.Where(x => x.designation == request.designation);
            }
            if (request.MAGASINId > 0)
            {
                data = data.Where(x => x.MAGASINId == request.MAGASINId);
            }
            return Ok(new { result = data });
        }

        [HttpPost]
        public IActionResult Post([FromBody] ARTICLE obj)
        {
            obj.isDeleted = 0;
            var data = ArticleDatabaseBusinessProvider.Instance.Add(obj);
            return Ok(new { success = true });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ARTICLE obj)
        {
            var data = ArticleDatabaseBusinessProvider.Instance.Update(id, obj);
            return Ok(new { success = true });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool success = false;
            try
            {
                ArticleDatabaseBusinessProvider.Instance.Remove(id);
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

