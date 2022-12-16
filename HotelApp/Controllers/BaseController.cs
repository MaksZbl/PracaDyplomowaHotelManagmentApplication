using HotelApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace HotelApp.Controllers
{
    public class BaseController: ControllerBase
    {
        protected LoggedInUser GetCurrentUser()
        {
            if (HttpContext.User.Identity is not ClaimsIdentity identity)
            {
                return null;
            }

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
    }
}
