using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelApp.EF;
using HotelApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Security.Cryptography;

namespace HotelApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggedInUsersController : BaseController
    {
        private readonly HotelAppDbContext _context;

        public LoggedInUsersController(HotelAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<IEnumerable<LoggedInUser>>> GetUsers()
        {
            try
            {
                var currentUser = GetCurrentUser();
                return Ok(await _context.Users.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userName}")]
        [Authorize(Roles = "Admin,Employee,Manager")]
        public async Task<ActionResult<LoggedInUser>> GetLoggedInUser(string userName)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
                return Ok(user);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> PutLoggedInUser(int id, LoggedInUser loggedInUser)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var userFromDb = _context.Users.FirstOrDefault(x => x.User_id == id);
                if (userFromDb != null)
                {
                    userFromDb.FirstName = loggedInUser.FirstName;
                    userFromDb.LastName = loggedInUser.LastName;
                    userFromDb.RegistrationDate = loggedInUser.RegistrationDate;
                    userFromDb.UserName = loggedInUser.UserName;
                    userFromDb.RoleValue = loggedInUser.RoleValue;
                    _context.Users.Update(userFromDb);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    _context.Users.Add(loggedInUser);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<LoggedInUser>> PostLoggedInUser(LoggedInUser loggedInUser)
        {
            try
            {
                var currentUser = GetCurrentUser();
                _context.Users.Add(loggedInUser);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteLoggedInUser(int id)
        {
            try
            {
                var currentUser = GetCurrentUser();

                var user = await _context.Users.FirstOrDefaultAsync(x => x.User_id == id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
