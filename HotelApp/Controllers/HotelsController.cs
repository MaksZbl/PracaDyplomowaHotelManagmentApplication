using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelApp.EF;
using HotelApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HotelApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly HotelAppDbContext _context;

        public HotelsController(HotelAppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            try
            {
                var currentUser = GetCurrentUser();
                return Ok(await _context.Hotels.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<Hotel>> GetHotel(string title)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var hotel = _context.Hotels.Where(x => x.Title.Contains(title));
                return Ok(hotel);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            try
            {
                var currentUser = GetCurrentUser();
                Hotel hotel1 = _context.Hotels.FirstOrDefault(x => x.Hotel_id == id);
                if (hotel1 != null)
                {
                    if(hotel.Description == null)
                    {
                       hotel1.Description = hotel1.Description;
                    }
                    else
                    {
                        hotel1.Description = hotel.Description;
                    }

                    if (hotel.Type == null)
                    {
                        hotel1.Type = hotel1.Type;
                    }
                    else
                    {
                        hotel1.Type = hotel.Type;
                    }

                    if (hotel.Address == null)
                    {
                        hotel1.Address = hotel1.Address;
                    }
                    else
                    {
                        hotel1.Address = hotel.Address;
                    }

                    if (hotel.Title == null)
                    {
                        hotel1.Title = hotel1.Title;
                    }
                    else
                    {
                        hotel1.Title = hotel.Title;
                    }

                    _context.Hotels.Update(hotel1);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    _context.Hotels.Add(hotel);
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            try
            {
                var currentUser = GetCurrentUser();
                _context.Hotels.Add(hotel);
                await _context.SaveChangesAsync();
                return Ok("$Hi admin");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var hotel = await _context.Hotels.FirstOrDefaultAsync(x => x.Hotel_id == id);
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        private LoggedInUser GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new LoggedInUser
                {
                    FirstName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value,
                    LastName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value,
                    EmailAdress = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                    UserName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    RoleValue = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value,
                };
            }
            else
            {
                return null;
            }
        }
    }
}
