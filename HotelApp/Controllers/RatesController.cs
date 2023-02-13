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
using System.Data;

namespace HotelApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : BaseController
    {
        private readonly HotelAppDbContext _context;

        public RatesController(HotelAppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{hotelId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rate>>> GetRatesOfHotel(int hotelId)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var ratesOfHotel = await _context.Rates.FirstOrDefaultAsync(x => x.HotelId == hotelId);
                if ( ratesOfHotel == null)
                {
                    return NotFound("Taki hotel nie istnieje");
                }

                var listOfGrades = await _context.Rates.ToListAsync();
                var gradeValue = 0.0;
                foreach(var grade in listOfGrades)
                {
                    gradeValue += grade.value;
                }

                var averageRate = gradeValue / listOfGrades.Count;
                

                return Ok(new { avRate = averageRate });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Rates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRate(int id, Rate rate)
        {
            if (id != rate.RateId)
            {
                return BadRequest();
            }

            _context.Entry(rate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "LoggedInUser")]
        [HttpPost]
        public async Task<ActionResult<Rate>> PostRate(Rate rate)
        {
            try
            {
                if(rate == null)
                {
                    throw new ArgumentNullException(nameof(rate));
                }
                var currentUser = GetCurrentUser();
                var currentRate = await _context.Rates.FirstOrDefaultAsync(x => x.LoggedInUserId == rate.LoggedInUserId);
                if(currentRate == null)
                {
                    _context.Rates.Add(rate);
                    
                    return CreatedAtAction("GetRate", new { id = rate.RateId }, rate);
                }

                currentRate.value = rate.value;
                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/Rates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRate(int id)
        {
            var rate = await _context.Rates.FindAsync(id);
            if (rate == null)
            {
                return NotFound();
            }

            _context.Rates.Remove(rate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RateExists(int id)
        {
            return _context.Rates.Any(e => e.RateId == id);
        }
    }
}
