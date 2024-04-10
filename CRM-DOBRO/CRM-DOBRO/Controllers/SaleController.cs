using CRM_DOBRO.Data;
using CRM_DOBRO.DTOs;
using CRM_DOBRO.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRM_DOBRO.Controllers
{
    [EnsureNotBlocked]
    [ApiController]
    [Route("api/sale")]
    public class SaleController(SaleService saleservice) : Controller
    {
        private readonly SaleService _saleservice = saleservice;

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            var sales = await _saleservice.GetSalesAsync();
            if(sales.Count == 0)
                return NoContent();
            return Ok(sales);
        }

        [Authorize(Roles = "Saler")]
        [HttpGet("saler/sales")]
        public async Task<IActionResult> GetMySales()
        {
            int salerId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var sales = await _saleservice.GetMySalesAsync(salerId);
            if (sales.Count == 0)
                return NoContent();
            return Ok(sales);
        }

        [Authorize(Roles = "Saler")]
        [HttpPost]
        public async Task<IActionResult> SaleCreating(SaleSetDTO sale)
        {
            int salerId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var newSale = await _saleservice.CreateSaleAsync(salerId, sale);
            if (newSale == null)
                return NotFound();
            return Created();

        }
    }
}


