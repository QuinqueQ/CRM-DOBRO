namespace WebApi.Controllers;

[EnsureNotBlocked]
[ApiController]
[Route("api/sale")]
public class SaleController(SaleService saleservice) : Controller
{
    [Authorize(Roles = "Admin")]
    [HttpGet("sales")]
    public async Task<IActionResult> GetSales()
    {
        var sales = await saleservice.GetSalesAsync();
        if(sales.Count == 0)
            return NoContent();
        return Ok(sales);
    }

    [Authorize(Roles = "Saler")]
    [HttpGet("saler/sales")]
    public async Task<IActionResult> GetMySales()
    {
        int salerId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var sales = await saleservice.GetMySalesAsync(salerId);
        if (sales.Count == 0)
            return NoContent();
        return Ok(sales);
    }

    [Authorize(Roles = "Saler")]
    [HttpPost]
    public async Task<IActionResult> SaleCreating(SaleSetDTO sale)
    {
        int salerId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        Sale? newSale = await saleservice.CreateSaleAsync(salerId, sale);
        if (newSale == null)
            return NotFound();
        return Created();

    }
}


