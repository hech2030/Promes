using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyTeam.Common.Models;
using MyTeam.Common.Models.Context;
using MyTeam.Common.Requests.bo.Stock;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyTeam.Controllers
{
    [Route("api/fw/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private  DbContext _StockContext;

        public StockController(ILogger<StockController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost]
        [Route("FindStock")]
        public ActionResult FindStock()
        {
            try
            {
                IList<Product> result = new List<Product>();
                result.Add(new Product
                {
                    productRef = "P1",
                    productName = "Produit 1"
                });
                result.Add(new Product
                {
                    productRef = "P2",
                    productName = "Produit 2"
                });
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = "Exception has been occured : " + ex.Message });
            }
        }
    }
}
