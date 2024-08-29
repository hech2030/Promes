using DataAcess.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTeam.Common.Requests.bo.Agence;
using System;
using System.Threading.Tasks;

namespace MyTeam.Controllers
{
    [Route("api/fw/[controller]")]
    [ApiController]
    public class AgencesController : ControllerBase
    {
        private readonly IAgenceDataBaseBusinessProvider _agencesProvider;

        public AgencesController(IAgenceDataBaseBusinessProvider agencesProvider)
        {
            _agencesProvider = agencesProvider;
        }

        [HttpPost]
        [Authorize]
        [Route("FindAgence")]
        public async Task<ActionResult> FindAgence(AgenceFindRequest Request)
        {
            try
            {
                var result = await _agencesProvider.Find(Request.id, Request.ville);
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = "Exception has been occured : " + ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        [Route("SaveAgence")]
        public async Task<ActionResult> SaveAgence(AgenceSaveRequest Request)
        {
            try
            {
                return Ok(await _agencesProvider.Save(Request.value));
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = ex.HelpLink });
            }
        }

        [HttpPost]
        [Authorize]
        [Route("DeleteAgence")]
        public async Task<ActionResult> DeleteAgence(AgenceDeleteRequest Request)
        {
            try
            {
                return Ok(await _agencesProvider.Remove(Request.id));
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = "Exception has been occured : " + ex.Message });
            }
        }
    }
}