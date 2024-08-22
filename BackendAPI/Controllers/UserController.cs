using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseServerContext _context;

        public UserController(DatabaseServerContext context)
        {
            _context = context;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {
            try
            {
                var user = new User
                {
                   
                    Username = addUserRequest.Username,
                    Fullname = addUserRequest.FullName,
                    DateOfBirth = addUserRequest.DateOfBirth,
                    Gender = addUserRequest.Gender,
                    PasswordHash = addUserRequest.PasswordHash
                };

                // Check if the user already exists by username
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

                if (existingUser == null)
                {
                    // User doesn't exist, add a new one
                    await _context.Users.AddAsync(user);
                }
                else
                {
                    // User exists, update the existing user
                    existingUser.Fullname = user.Fullname;
                    existingUser.DateOfBirth = user.DateOfBirth;
                    existingUser.Gender = user.Gender;
                    existingUser.PasswordHash = user.PasswordHash;

                    _context.Users.Update(existingUser);
                }

                await _context.SaveChangesAsync();
                return Ok("Success");
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                return BadRequest(innerException?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {

                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                var userRoles = await _context.UserRoles.Where(ur => ur.UserId == id).ToListAsync();
                _context.UserRoles.RemoveRange(userRoles);
                await _context.SaveChangesAsync();

                var deleteUser = _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Ok("Success");
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                return BadRequest(innerException?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
