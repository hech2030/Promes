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
    public class SORTIEsController : ControllerBase
    {
        private readonly ISortieDatabaseBusinessProvider _sortieProvider;

        public SORTIEsController(ISortieDatabaseBusinessProvider sortieProvider)
        {
            _sortieProvider = sortieProvider;
        }

        [HttpPost]
        [Route("FindSORTIE")]
        public async Task<IActionResult> findSORTIE(SortieFindRequest request)
        {
            var data = await _sortieProvider.Find(request.id);
            if (request.numSortie > 0)
            {
                data = data.Where(x => x.numSortie == request.numSortie).ToList();
            }
            if (request.ARTICLEId > 0)
            {
                data = data.Where(x => x.ARTICLEId == request.ARTICLEId).ToList();
            }
            return Ok(new { result = data });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SORTIE obj)
        {
            _ = await _sortieProvider.Add(obj);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SORTIE obj)
        {
            var _ = await _sortieProvider.Update(id, obj);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sortieProvider.Remove(id);
            return Ok();
        }
    }
}