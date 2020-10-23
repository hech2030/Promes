using DataAcess.Business;
using DataAcess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTeam.Common.Requests.bo.Stock.Mouvement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTeam.Controllers
{
    [Route("api/fw/[controller]")]
    [ApiController]
    public class MOUVEMENTsController : ControllerBase
    {
        public MOUVEMENTsController()
        {
        }
        [HttpPost]
        [Authorize]
        [Route("SaveMouvements")]
        public ActionResult SaveMouvement(MouvementSaveRequest Request)
        {
            try
            {
                foreach (var item in Request.Values)
                {
                    switch (item.Type.id)
                    {
                        //Case 1 is entry
                        case 1:
                            {
                                EntreeDatabaseBusinessProvider.Instance.Add(new ENTREE()
                                {
                                    ARTICLE = item.Article,
                                    ARTICLEId = item.Article.Id,
                                    prixDentree = item.Prix,
                                    quantite = item.Quantite
                                });
                                break;
                            }
                        //Case 2 is out
                        case 2:
                            {
                                SortieDatabaseBusinessProvider.Instance.Add(new SORTIE()
                                {
                                    ARTICLE = item.Article,
                                    ARTICLEId = item.Article.Id,
                                    prixDSortie = item.Prix,
                                    quantite = item.Quantite
                                });
                                break;
                            }
                    }
                }
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = ex.HelpLink });
            }
        }
    }
}
