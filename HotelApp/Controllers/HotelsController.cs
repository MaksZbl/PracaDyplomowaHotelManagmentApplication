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
    public class HotelsController : BaseController
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
                var hotelFromDb = _context.Hotels.FirstOrDefault(x => x.Hotel_id == id);
                if (hotelFromDb != null)
                {
                    hotelFromDb.Type = hotel.Type;
                    hotelFromDb.Title = hotel.Title;
                    hotelFromDb.Description = hotel.Description;
                    hotelFromDb.Address = hotel.Address;
                    _context.Hotels.Update(hotelFromDb);
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
    }
}
