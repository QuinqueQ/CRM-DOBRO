using CRM_DOBRO.Data;
using CRM_DOBRO.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM_DOBRO.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : Controller 
    {
        private readonly AdminService _adminService;
        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet]
        public async Task <IActionResult> Registration()
        {
            //await _adminService.NewAdmin();
            return Ok();
        }

    }
}
