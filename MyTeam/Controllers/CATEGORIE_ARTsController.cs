using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAcess.Business;
using DataAcess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTeam.Common.Requests.bo.Stock.Categorie_Art;
using MyTeam.Common.Requests.bo.Users;

namespace MyTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CATEGORIE_ARTsController : ControllerBase
    {
        public CATEGORIE_ARTsController()
        {
        }

        [HttpPost]
        [Authorize]
        [Route("FindCATEGORIE_ART")]
        public IActionResult FindCATEGORIE_ART(CATEGORIE_ARTFindRequest request)
        {
            var data = CategorieArtDatabaseBusinessProvider.Instance.Find(request.id,request.nomCategorie);
            return Ok(new { result = data });
        }

        [HttpPost]
        [Authorize]
        [Route("SaveCategorie")]
        public IActionResult SaveCateogorie(CategorieSaveRequest request)
        {
            try
            {
                return Ok(CategorieArtDatabaseBusinessProvider.Instance.Save(request.value));
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = ex.HelpLink });
            }
        }


        [HttpPost]
        [Authorize]
        [Route("DeleteCategorie")]
        public IActionResult DeleteCategorie(CategorieDeleteRequest request)
        {
            bool success = false;
            try
            {
                CategorieArtDatabaseBusinessProvider.Instance.Remove(request.id);
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

