using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAcess.Business;
using DataAcess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTeam.Common.Requests.bo.Stock.Magasin;
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
        [Authorize]
        [Route("FindMagasin")]
        public ActionResult FindMagasin(MAGASINFindRequest Request)
        {
            try
            {
                var result = MagasinDatabaseBusinessProvider.Instance.Find(Request.id, Request.NomMagasin);
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
        [Route("SaveMagasin")]
        public ActionResult SaveMagasin(MagasinSaveRequest Request)
        {
            try
            {
                return Ok(MagasinDatabaseBusinessProvider.Instance.Save(Request.value));
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = ex.HelpLink });
            }
        }
        [HttpPost]
        [Authorize]
        [Route("DeleteMagasin")]
        public ActionResult DeleteAgence(MagasinDeleteRequest Request)
        {
            try
            {
                return Ok(MagasinDatabaseBusinessProvider.Instance.Remove(Request.id));
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = "Exception has been occured : " + ex.Message });
            }
        }
    }
}

