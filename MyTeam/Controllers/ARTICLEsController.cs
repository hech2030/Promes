using DataAcess.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTeam.Common.Requests.bo.Stock.Article;
using MyTeam.Common.Requests.bo.Users;
using System;

namespace MyTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ARTICLEsController : ControllerBase
    {
        public ARTICLEsController()
        {
        }

        [HttpPost]
        [Authorize]
        [Route("FindArticle")]
        public ActionResult FindArticle(ArticleFindRequest Request)
        {
            try
            {
                var result = ArticleDatabaseBusinessProvider.Instance.Find(Request.id, Request.MAGASINId,Request.designation);
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
        public ActionResult SaveArticle(ArticleSaveRequest Request)
        {
            try
            {
                return Ok(ArticleDatabaseBusinessProvider.Instance.Save(Request.value));
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
        public ActionResult DeleteArticle(ArticleRemoveRequest Request)
        {
            try
            {
                return Ok(ArticleDatabaseBusinessProvider.Instance.Remove(Request.Id));
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = "Exception has been occured : " + ex.Message });
            }
        }
    }
}

