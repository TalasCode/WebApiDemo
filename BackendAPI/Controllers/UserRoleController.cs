using BackendAPI.Models;
using BackendAPI.RequestBody;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserRoleController : Controller
    {
        private readonly DatabaseServerContext _context;
        public UserRoleController(DatabaseServerContext context)
        {
            _context = context;
        }
        [HttpGet("GetUsersRoles")]
        public async Task<ActionResult<IEnumerable<UserRole>>> GetUsersRoles()
        {
            try
            {
                var usersRoles = await _context.UserRoles.ToListAsync();
                return Ok(usersRoles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

    }
}
