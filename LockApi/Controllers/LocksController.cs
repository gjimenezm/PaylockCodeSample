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
    public class LocksController : ControllerBase
    {
        private readonly LocksContext _context;

        public LocksController(LocksContext context)
        {
            _context = context;
        }

        // GET: api/Locks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lock>>> GetLocks()
        {
            return await _context.Locks.ToListAsync();
        }

        // GET: api/Locks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lock>> GetLock(long id)
        {
            var @lock = await _context.Locks.FindAsync(id);

            if (@lock == null)
            {
                return NotFound();
            }

            return @lock;
        }

        // PUT: api/Locks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLock(long id, Lock @lock)
        {
            if (id != @lock.Id)
            {
                return BadRequest();
            }

            _context.Entry(@lock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LockExists(id))
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

        // POST: api/Locks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Lock>> PostLock(Lock @lock)
        {
            _context.Locks.Add(@lock);
            await _context.SaveChangesAsync();

            return CreatedAtAction( nameof(GetLock), new { id = @lock.Id }, @lock);
        }

        // DELETE: api/Locks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lock>> DeleteLock(long id)
        {
            var @lock = await _context.Locks.FindAsync(id);
            if (@lock == null)
            {
                return NotFound();
            }

            _context.Locks.Remove(@lock);
            await _context.SaveChangesAsync();

            return @lock;
        }

        private bool LockExists(long id)
        {
            return _context.Locks.Any(e => e.Id == id);
        }
    }
}
