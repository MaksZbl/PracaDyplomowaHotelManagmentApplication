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
using System.Text;
using System.Security.Cryptography;

namespace HotelApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : BaseController
    {
        private readonly HotelAppDbContext _context;

        public PaymentsController(HotelAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Payments
        [Authorize(Roles = "Admin, LoggedInUser, Manager, Employee")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            try
            {
                var currentUser = GetCurrentUser();
                if (currentUser.RoleValue != "Admin")
                {
                    if(currentUser.RoleValue == "Employee" || currentUser.RoleValue == "Manager")
                    {
                        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == currentUser.UserName);
                        return Ok(_context.Payments.Where(x => x.Booking.Room.HotelId == user.HotelId));
                    }
                    return Ok(_context.Payments.Where(x => x.Booking.LoggedInUser.UserName == currentUser.UserName));
                }
                return Ok(await _context.Bookings.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, Payment payment)
        {
            if (id != payment.Payment_id)
            {
                return BadRequest();
            }

            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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

        // POST: api/Payments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin, LoggedInUser, Employee")]
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
        {
            var currentUser = GetCurrentUser();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == currentUser.UserName);
            var bookingFromDb = await _context.Bookings.FirstOrDefaultAsync(x => x.Booking_id == payment.Booking_id);
            if( bookingFromDb == null)
            {
                return NotFound("bookingFromDb is null");
            }
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            var lastId = _context.Payments.Max(x => x.Payment_id);
            var lastPayment = await _context.Payments.FirstOrDefaultAsync(x => x.Payment_id == lastId);
            bookingFromDb.PaymentId = lastPayment.Payment_id;
            bookingFromDb.IsPayed = true;
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPayment", new { id = payment.Payment_id }, payment);
        }
       

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Payment_id == id);
        }
    }
}
