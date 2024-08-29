using DataAcess.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTeam.Common.Requests.bo.Stock.Categorie_Art;
using MyTeam.Common.Requests.bo.Users;
using System;
using System.Threading.Tasks;

namespace MyTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CATEGORIE_ARTsController : ControllerBase
    {
        private readonly ICategorieArtDatabaseBusinessProvider _categoryProvider;

        public CATEGORIE_ARTsController(ICategorieArtDatabaseBusinessProvider categoryProvider)
        {
            _categoryProvider = categoryProvider;
        }

        [HttpPost]
        [Authorize]
        [Route("FindCATEGORIE_ART")]
        public async Task<IActionResult> FindCATEGORIE_ART(CATEGORIE_ARTFindRequest request)
        {
            var data = await _categoryProvider.Find(request.id, request.nomCategorie);
            return Ok(new { result = data });
        }

        [HttpPost]
        [Authorize]
        [Route("SaveCategorie")]
        public async Task<IActionResult> SaveCateogorie(CategorieSaveRequest request)
        {
            try
            {
                return Ok(await _categoryProvider.Save(request.value));
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
        public async Task<IActionResult> DeleteCategorie(CategorieDeleteRequest request)
        {
            bool success = false;
            try
            {
                await _categoryProvider.Remove(request.id);
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