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

namespace HotelApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : BaseController
    {
        private readonly HotelAppDbContext _context;

        public BookingsController(HotelAppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Authorize(Roles = "Admin, LoggedInUser, Employee")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            try
            {
                var currentUser = GetCurrentUser();
                if(currentUser.RoleValue != "Admin")
                {
                    return Ok(_context.Bookings.Where(x => x.LoggedInUser.UserName == currentUser.UserName));
                }
                return Ok(await _context.Bookings.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, LoggedInUser, Employee")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var bookFromDb = _context.Bookings.FirstOrDefault(x => x.Booking_id == id);

                if (bookFromDb == null)
                {
                    _context.Bookings.Add(booking);
                    await _context.SaveChangesAsync();
                    return Ok();
                }

                bookFromDb.Type = booking.Type;
                bookFromDb.Title = booking.Title;
                bookFromDb.Description = booking.Description;
                bookFromDb.StartDate = booking.StartDate;
                bookFromDb.EndDate = booking.EndDate;
                bookFromDb.LoggedInUserId = booking.LoggedInUserId;
                _context.Bookings.Update(bookFromDb);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin, LoggedInUser, Employee")]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            try
            {
                var checkRoom = _context.Rooms.FirstOrDefault(x=>x.IsFree==true && x.Hotel.Hotel_id == int.Parse(booking.Description) && x.Type == booking.Type);
                var currentUser = GetCurrentUser();

                if (checkRoom == null || checkRoom.IsFree != true)
                {
                    return NotFound();
                }
           
                checkRoom.IsFree = false;
                if(currentUser.RoleValue == "LoggedInUser" || currentUser.RoleValue == "Employee")
                {
                    var checkUser = _context.Users.FirstOrDefault(x => x.UserName == currentUser.UserName);
                    booking.RoomId = checkRoom.Room_id;
                    booking.LoggedInUserId = checkUser.User_id;
                }
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
                var lastId = _context.Bookings.Max(x => x.Booking_id);
                var lastBook = _context.Bookings.FirstOrDefault(x => x.Booking_id == lastId);
                checkRoom.BookingId = lastBook.Booking_id;
                lastBook.Title = $"Room reservation #{booking.Booking_id}";
                lastBook.Description = $"Room reservation #{booking.Booking_id}";
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
        [Authorize(Roles = "Admin, LoggedInUser")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var book = await _context.Bookings.FirstOrDefaultAsync(x => x.Booking_id == id);
                var checkRoom = _context.Rooms.FirstOrDefault(x => x.Room_id == book.RoomId);
                checkRoom.IsFree = true;
                _context.Bookings.Remove(book);
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
