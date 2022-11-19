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
    public class CustomersController : ControllerBase
    {
        private readonly HotelAppDbContext _context;

        public CustomersController(HotelAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            try
            {
                var currentUser = GetCurrentUser();
                return Ok(await _context.Customers.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.User_id == id);
                return Ok(customer);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var customer1 = _context.Customers.FirstOrDefault(x => x.User_id == id);
                if (customer1 != null)
                {
                    customer1.Adress = customer.Adress;
                    customer1.Mobile = customer.LastName;
                    _context.Customers.Update(customer1);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    _context.Customers.Add(customer);
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
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            try
            {
                var currentUser = GetCurrentUser();
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.User_id == id);
                _context.Users.Remove(customer);
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
