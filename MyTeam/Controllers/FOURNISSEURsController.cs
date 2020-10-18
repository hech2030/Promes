using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAcess.Business;
using DataAcess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTeam.Common.Requests.bo.Stock.Fournisseur;
using MyTeam.Common.Requests.bo.Users;

namespace MyTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FOURNISSEURsController : ControllerBase
    {
        public FOURNISSEURsController()
        {
        }
        [HttpPost]
        [Authorize]
        [Route("FindFournisseur")]
        public IActionResult FindCATEGORIE_ART(FOURNISSEURFindRequest request)
        {
            var data = FournisseurDatabaseBusinessProvider.Instance.Find(request.id, request.nomFournisseur);
            return Ok(new { result = data });
        }

        [HttpPost]
        [Authorize]
        [Route("SaveFournissuer")]
        public IActionResult SaveCateogorie(FournisseurSaveRequest request)
        {
            try
            {
                return Ok(FournisseurDatabaseBusinessProvider.Instance.Save(request.value));
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = ex.HelpLink });
            }
        }


        [HttpPost]
        [Authorize]
        [Route("DeleteFournisseur")]
        public IActionResult DeleteCategorie(FournissuerDeleteRequest request)
        {
            bool success = false;
            try
            {
                FournisseurDatabaseBusinessProvider.Instance.Remove(request.id);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { success, ex.HelpLink });
            }
        }
    }
}

