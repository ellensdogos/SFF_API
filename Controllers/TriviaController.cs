using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SFF_API.Context;
using SFF_API.Models;

namespace SFF_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriviaController : ControllerBase
    {
        private readonly SFFContext _context;

        public TriviaController(SFFContext context)
        {
            _context = context;
        }

        // GET: api/Trivia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trivia>>> GetTrivias()
        {
            //Returnerar alla trivias och inkluderar detaljerna från filmen som trivian hör till
            return await _context.Trivias
                                .Include(m => m.Movie)
                                .ToListAsync();
        }

        // GET: api/Trivia/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Trivia>> GetTrivia(long Id)
        {
            var trivia = await _context.Trivias.FindAsync(Id);

            //Kollar om Id:t som skickats in finns hos en trivia
            if (trivia == null)
            {
                return NotFound();
            }

            return trivia;
        }

        // POST: api/Trivia
        [HttpPost]
        public async Task<ActionResult<Trivia>> PostTrivia(Trivia trivia)
        {
            var movie = await _context.Movies.FindAsync(trivia.MovieId);

            //Lägger till en ny trivia och sätter filmens Id till trivians filmId för att koppla dom samman
            trivia.MovieId = movie.Id;
            _context.Trivias.Add(trivia);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("Trivia", new { id = trivia.Id }, trivia);
            return CreatedAtAction(nameof(GetTrivia), new { id = trivia.Id }, trivia);
        }

        // DELETE: api/Trivia/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Trivia>> DeleteTrivia(long Id)
        {
            //Söker efter en trivia med det ID som skickats in
            var trivia = await _context.Trivias.FindAsync(Id);

            //Tar bort trivian och sparar ändringarna
            _context.Trivias.Remove(trivia);
            await _context.SaveChangesAsync();

            return trivia;
        }
    }
}
