using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SFF_API.Models;
using SFF_API.Context;

namespace SFF_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudioController : ControllerBase
    {
        private readonly SFFContext _context;

        public StudioController(SFFContext context)
        {
            _context = context;
        }

        // GET: api/Studio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Studio>>> GetStudios()
        {
            return await _context.Studios.ToListAsync();
        }

        // GET: api/Studio/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Studio>> GetStudio(long Id)
        {
            var studio = await _context.Studios.FindAsync(Id);

            if (studio == null)
            {
                return NotFound();
            }

            return studio;
        }

        // PUT: api/Studio/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudio(long id, Studio studio)
        {
            //Kollar om Id:t stämmer med vald studioId
            if (id != studio.Id)
            {
                return BadRequest();
            }

            //Kollar om studion finns
            if (studio == null)
            {
                return NotFound();
            }

            _context.Entry(studio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(studio);
        }

        // POST: api/Studio
        [HttpPost]
        public async Task<ActionResult<Studio>> PostStudio(Studio studio)
        {
            _context.Studios.Add(studio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudio), new { id = studio.Id }, studio);
        }

        // DELETE: api/Studio/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Studio>> DeleteStudio(long id)
        {
            var studio = await _context.Studios.FindAsync(id);
            _context.Studios.Remove(studio);
            await _context.SaveChangesAsync();
            return studio;
        }
    }
}
