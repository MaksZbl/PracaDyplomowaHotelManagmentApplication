using HotelApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System;
using HotelApp.EF;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HotelApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : BaseController
    {
        private readonly HotelAppDbContext _context;

        public RegistrationController(HotelAppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(LoggedInUser user)
        {
            try
            {
                var valideUser = CreateUser(user);
                if (valideUser == null)
                {
                    return BadRequest();
                }
                SHA256 sha256 = SHA256.Create();
                byte[] b = Encoding.ASCII.GetBytes(valideUser.Password);
                byte[] hash = sha256.ComputeHash(b);
                StringBuilder sb = new StringBuilder();
                foreach (var item in hash)
                {
                    sb.Append(item.ToString("X2"));
                }
                valideUser.Password = Convert.ToString(sb);
                valideUser.RoleValue = "LoggedInUser";
                _context.Users.Add(valideUser);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private LoggedInUser CreateUser(LoggedInUser user)
        {
            var context = new ValidationContext(user);
            var userFromDb = _context.Users.FirstOrDefault(x=>x.UserName == user.UserName && x.EmailAdress == user.EmailAdress && x.Mobile == user.Mobile);
            var results = new List<ValidationResult>();
            
            if (!Validator.TryValidateObject(user, context, results, true) && userFromDb != null)
            {
                return null;
            }

            
                return user;
        }
    }
}

