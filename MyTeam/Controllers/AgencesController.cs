using DataAcess.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTeam.Common.Requests.bo.Agence;
using System;

namespace MyTeam.Controllers
{
    [Route("api/fw/[controller]")]
    [ApiController]
    public class AgencesController : ControllerBase
    {
        public AgencesController()
        {
        }


        [HttpPost]
        [Authorize]
        [Route("FindAgence")]
        public ActionResult FindAgence(AgenceFindRequest Request)
        {
            try
            {
                var result = AgenceDataBaseBusinessProvider.Instance.Find(Request.id,Request.ville);
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
        public ActionResult SaveAgence(AgenceSaveRequest Request)
        {
            try
            {
                return Ok(AgenceDataBaseBusinessProvider.Instance.Save(Request.value));
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = "Exception has been occured : " + ex.Message });
            }
        }        
        [HttpPost]
        [Authorize]
        [Route("DeleteAgence")]
        public ActionResult DeleteAgence(AgenceDeleteRequest Request)
        {
            try
            {
                return Ok(AgenceDataBaseBusinessProvider.Instance.Remove(Request.id));
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = "Exception has been occured : " + ex.Message });
            }
        }
    }
}
