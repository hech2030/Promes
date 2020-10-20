using DataAcess.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTeam.Common.Requests.bo.SousTraitant;
using System;
using System.Linq;

namespace MyTeam.Controllers
{
    [Route("api/fw/[controller]")]
    [ApiController]
    public class SousTraitantsController : ControllerBase
    {
        public SousTraitantsController()
        {
        }


        [HttpPost]
        [Authorize]
        [Route("FindSousTraitant")]
        public ActionResult FindSousTraitant(SousTraitantFindRequest Request)
        {
            try
            {
                var result = SousTraitantDataBaseBusinessProvider.Instance.Find(Request.id,Request.ville);
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
        [Route("SaveSousTraitant")]
        public ActionResult SaveSousTraitant(SousTraitantSaveRequest Request)
        {
            try
            {
                return Ok(SousTraitantDataBaseBusinessProvider.Instance.Save(Request.value));
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = ex.HelpLink });
            }
        }        
        [HttpPost]
        [Authorize]
        [Route("DeleteSousTraitant")]
        public ActionResult DeleteSousTraitant(SousTraitantDeleteRequest Request)
        {
            try
            {
                return Ok(SousTraitantDataBaseBusinessProvider.Instance.Remove(Request.id));
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = "Exception has been occured : " + ex.Message });
            }
        }
    }
}
