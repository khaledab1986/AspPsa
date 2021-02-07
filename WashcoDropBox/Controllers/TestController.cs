using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WashcoDropBox.Models;

namespace WashcoDropBox.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly WashcoDbContext _context;

        public TestController(WashcoDbContext context)
        {
            _context = context;
        }

        // GET: api/Test
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WashcoBox>>> GetWashcoBoxes()
        {
            return await _context.WashcoBoxes.ToListAsync();
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WashcoBox>> GetWashcoBox(long id)
        {
            var washcoBox = await _context.WashcoBoxes.FindAsync(id);

            if (washcoBox == null)
            {
                return NotFound();
            }

            return washcoBox;
        }

        // PUT: api/Test/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWashcoBox(long id, WashcoBox washcoBox)
        {
            if (id != washcoBox.Id)
            {
                return BadRequest();
            }

            _context.Entry(washcoBox).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WashcoBoxExists(id))
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

        // POST: api/Test
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WashcoBox>> PostWashcoBox(WashcoBox washcoBox)
        {
            _context.WashcoBoxes.Add(washcoBox);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWashcoBox", new { id = washcoBox.Id }, washcoBox);
        }

        // DELETE: api/Test/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WashcoBox>> DeleteWashcoBox(long id)
        {
            var washcoBox = await _context.WashcoBoxes.FindAsync(id);
            if (washcoBox == null)
            {
                return NotFound();
            }

            _context.WashcoBoxes.Remove(washcoBox);
            await _context.SaveChangesAsync();

            return washcoBox;
        }

        private bool WashcoBoxExists(long id)
        {
            return _context.WashcoBoxes.Any(e => e.Id == id);
        }
    }
}
