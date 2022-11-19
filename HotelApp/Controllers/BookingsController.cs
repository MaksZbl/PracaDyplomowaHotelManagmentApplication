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

namespace HotelApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly HotelAppDbContext _context;

        public BookingsController(HotelAppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            try
            {
                var currentUser = GetCurrentUser();
                if(currentUser.RoleValue != "Admin")
                {
                    return Ok(await _context.Bookings.FirstOrDefaultAsync(x => x.Customer.UserName == currentUser.UserName));
                }
                return Ok(await _context.Bookings.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var book = await _context.Bookings.FirstOrDefaultAsync(x => x.Booking_id == id);
                return Ok(book);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var book1 = _context.Bookings.FirstOrDefault(x => x.Booking_id == id);
                if (book1 != null)
                {
                    book1.Type = booking.Type;
                    book1.Title = booking.Title;
                    book1.Description = booking.Description;
                    book1.Date = booking.Date;
                    book1.Customer_id = booking.Customer_id;
                    _context.Bookings.Update(book1);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    _context.Bookings.Add(booking);
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
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            try
            {
                var currentUser = GetCurrentUser();
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var book = await _context.Bookings.FirstOrDefaultAsync(x => x.Booking_id == id);
                _context.Bookings.Remove(book);
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
