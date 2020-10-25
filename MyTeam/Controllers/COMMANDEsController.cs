using DataAcess.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTeam.Common.Requests.bo.Stock.Commande;
using System;

namespace MyTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class COMMANDEsController : ControllerBase
    {
        public COMMANDEsController()
        {
        }


        [HttpPost]
        [Authorize]
        [Route("FindCommande")]
        public ActionResult FindCommande(CommandFindRequest Request)
        {
            try
            {
                var result = CommandeDatabaseBusinessProvider.Instance.Find(Request.Id, Request.Fournisseur, Request.date);
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
        [Route("SaveCommande")]
        public ActionResult SaveCommande(CommandSaveRequest Request)
        {
            try
            {
                return Ok(CommandeDatabaseBusinessProvider.Instance.Save(Request.Value));
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = ex.HelpLink });
            }
        }
        [HttpPost]
        [Authorize]
        [Route("DeleteCommande")]
        public ActionResult DeleteCommande(CommandDeleteRequest Request)
        {
            try
            {
                return Ok(CommandeDatabaseBusinessProvider.Instance.Remove(Request.Id));
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = "Exception has been occured : " + ex.Message });
            }
        }
    }
}

