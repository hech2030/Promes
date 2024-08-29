using DataAcess.Business.Interfaces;
using DataAcess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTeam.Common.Requests.bo.Stock.Mouvement;
using System;
using System.Threading.Tasks;

namespace MyTeam.Controllers
{
    [Route("api/fw/[controller]")]
    [ApiController]
    public class MOUVEMENTsController : ControllerBase
    {
        private readonly IEntreeDatabaseBusinessProvider _entreeProvider;
        private readonly ISortieDatabaseBusinessProvider _sortieProvider;

        public MOUVEMENTsController(IEntreeDatabaseBusinessProvider entreeProvider, ISortieDatabaseBusinessProvider sortieProvider)
        {
            _entreeProvider = entreeProvider;
            _sortieProvider = sortieProvider;
        }

        [HttpPost]
        [Authorize]
        [Route("SaveMouvements")]
        public async Task<ActionResult> SaveMouvementAsync(MouvementSaveRequest Request)
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
                                await _entreeProvider.Add(new ENTREE()
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
                                await _sortieProvider.Add(new SORTIE()
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