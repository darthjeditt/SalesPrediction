using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesPrediction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesPrediction.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        private CoreDbContext _dbContext;

        public UserController(CoreDbContext dbContext) {
            _dbContext = dbContext;
        }

        [HttpGet("GetHistory")]
        public IActionResult Get() {
            try {
                var sales = _dbContext.TblSales.ToList();
                if(sales.Count == 0) {
                    return StatusCode(404, "no user found");
                }
                return Ok(sales);

            } 
            catch (Exception ex) {
                return StatusCode(500, "An error has occurred");
            }
        }

        [HttpPost("CreateHistory")]
        public IActionResult Create([FromBody] UserRequest request) {
            TblSales sale = new TblSales();
            sale.Dos = request.Dos;
            sale.Sales = request.Sales;

            try {
                _dbContext.TblSales.Add(sale);
                _dbContext.SaveChanges();
            } 
            catch (Exception ex) {
                return StatusCode(500, "An error has occurred");
            }
            var sales = _dbContext.TblSales.ToList();
            return Ok(sales);
        }
    }
}
