using Microsoft.AspNetCore.Mvc;

namespace CRM_DOBRO.Controllers
{
    [ApiController]
    [Route("api/sale")]
    public class SaleController : Controller
    {
        public SaleController()
        {
            
        }

        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            return Ok();
        }

        [HttpGet("user/sales")]
        public async Task<IActionResult> GetMySales()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SaleCreating()
        {
            return Ok();
        }

    }
}
//Продажа:
//-Просмотр всех продаж(доступно: админ)
//- Просмотр своих продаж (доступно: продажник)
//- Создание продажи (доступно: продажник)

