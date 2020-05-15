using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LockApi.Models;

namespace LockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserxLocksController : ControllerBase
    {
        private readonly LocksContext _context;

        public UserxLocksController(LocksContext context)
        {
            _context = context;
        }

        // GET: api/UserxLocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserxLock>>> GetUserxLock()
        {
            return await _context.UserxLock.ToListAsync();
        }

        // GET: api/UserxLocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserxLock>> GetUserxLock(long id)
        {
            var userxLock = await _context.UserxLock.FindAsync(id);

            if (userxLock == null)
            {
                return NotFound();
            }

            return userxLock;
        }

        // PUT: api/UserxLocks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserxLock(long id, UserxLock userxLock)
        {
            if (id != userxLock.Id)
            {
                return BadRequest();
            }

            _context.Entry(userxLock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserxLockExists(id))
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

        // POST: api/UserxLocks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserxLock>> PostUserxLock(UserxLock userxLock)
        {
            _context.UserxLock.Add(userxLock);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserxLock), new { id = userxLock.Id }, userxLock);
        }

        // DELETE: api/UserxLocks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserxLock>> DeleteUserxLock(long id)
        {
            var userxLock = await _context.UserxLock.FindAsync(id);
            if (userxLock == null)
            {
                return NotFound();
            }

            _context.UserxLock.Remove(userxLock);
            await _context.SaveChangesAsync();

            return userxLock;
        }

        private bool UserxLockExists(long id)
        {
            return _context.UserxLock.Any(e => e.Id == id);
        }
    }
}
