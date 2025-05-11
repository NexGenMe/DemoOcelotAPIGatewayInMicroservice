using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private static readonly string[] Users = new[]
        {
            "User1", "User2", "User3"
        };

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await Task.Delay(4000);
            return Ok(Users);
        }
    }
}
