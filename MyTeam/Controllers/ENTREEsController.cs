using DataAcess.Business.Interfaces;
using DataAcess.Models;
using Microsoft.AspNetCore.Mvc;
using MyTeam.Common.Requests.bo.Users;
using System.Linq;
using System.Threading.Tasks;

namespace MyTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ENTREEsController : ControllerBase
    {
        private readonly IEntreeDatabaseBusinessProvider _entreeProvider;

        public ENTREEsController(IEntreeDatabaseBusinessProvider entreeProvider)
        {
            _entreeProvider = entreeProvider;
        }

        [HttpPost]
        [Route("FindENTREE")]
        public async Task<IActionResult> FindENTREE(EntreeFindRequest request)
        {
            var data = await _entreeProvider.Find(request.id);
            if (request.numEntree > 0)
            {
                data = data.Where(x => x.numEntree == request.numEntree).ToList();
            }
            if (request.ARTICLEId > 0)
            {
                data = data.Where(x => x.ARTICLEId == request.ARTICLEId).ToList();
            }
            return Ok(new { result = data });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ENTREE obj)
        {
            _ = await _entreeProvider.Add(obj);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ENTREE obj)
        {
            _ = await _entreeProvider.Update(id, obj);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _entreeProvider.Remove(id);
            return Ok();
        }
    }
}