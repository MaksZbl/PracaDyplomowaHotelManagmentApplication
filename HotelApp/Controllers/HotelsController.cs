using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelApp.EF;
using HotelApp.Models;

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
                return Ok(await _context.Hotels.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            try
            {
                var hotel = await _context.Hotels.FirstOrDefaultAsync(x => x.Hotel_id == id);
                return Ok(hotel);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            try
            {
                Hotel hotel1 = _context.Hotels.FirstOrDefault(x => x.Hotel_id == id);
                if (hotel1 != null)
                {
                    hotel1.Description = hotel.Description;
                    hotel1.Type = hotel.Type;
                    hotel1.Address = hotel.Address;
                    hotel1.Rating = hotel.Rating;
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
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            try
            {
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
        public async Task<IActionResult> DeleteHotel(int id)
        {
            try
            {
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
