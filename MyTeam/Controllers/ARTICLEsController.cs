using DataAcess.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTeam.Common.Requests.bo.Stock.Article;
using MyTeam.Common.Requests.bo.Users;
using System;
using System.Threading.Tasks;

namespace MyTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ARTICLEsController : ControllerBase
    {
        private readonly IArticleDatabaseBusinessProvider _articlesProvider;

        public ARTICLEsController(IArticleDatabaseBusinessProvider articlesProvider)
        {
            _articlesProvider = articlesProvider;
        }

        [HttpPost]
        [Authorize]
        [Route("FindArticle")]
        public async Task<ActionResult> FindArticle(ArticleFindRequest Request)
        {
            try
            {
                var result = await _articlesProvider.Find(Request.id, Request.MAGASINId, Request.designation);
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
        [Route("SaveArticle")]
        public async Task<ActionResult> SaveArticle(ArticleSaveRequest Request)
        {
            try
            {
                return Ok(await _articlesProvider.Save(Request.value));
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = ex.HelpLink });
            }
        }

        [HttpPost]
        [Authorize]
        [Route("DeleteArticle")]
        public async Task<ActionResult> DeleteArticle(ArticleRemoveRequest Request)
        {
            try
            {
                return Ok(await _articlesProvider.Remove(Request.Id));
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = "Exception has been occured : " + ex.Message });
            }
        }
    }
}