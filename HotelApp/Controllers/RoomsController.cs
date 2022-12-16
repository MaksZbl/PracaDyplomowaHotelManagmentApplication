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
    public class RoomsController : BaseController
    {
        private readonly HotelAppDbContext _context;

        public RoomsController(HotelAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            try
            {
                var currentUser = GetCurrentUser();
                return Ok(await _context.Rooms.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Room_id == id);
                return Ok(room);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            try
            {
                var currentUser = GetCurrentUser();
                Room room1 = _context.Rooms.FirstOrDefault(x => x.Room_id == id);
                if (room1 != null)
                {
                    room1.Number = room.Number;
                    room1.Description = room.Description;
                    room1.Type = room.Type;
                    room1.IsFree = room.IsFree;
                    _context.Rooms.Update(room1);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    _context.Rooms.Add(room);
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
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            try
            {
                var currentUser = GetCurrentUser();
                _context.Rooms.Add(room);
                await _context.SaveChangesAsync();
                return Ok("$Hi admin");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Room_id == id);
                _context.Rooms.Remove(room);
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
