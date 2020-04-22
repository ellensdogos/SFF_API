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
    public class MovieController : ControllerBase
    {
        private readonly SFFContext _context;

        public MovieController(SFFContext context)
        {
            _context = context;
        }


        // GET: api/Movie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            //Itererar genom movies och returnerar alla filmer
            return await _context.Movies.ToListAsync();
        }

        // GET: api/Movie/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(long Id)
        {
            var movie = await _context.Movies.FindAsync(Id);

            //Kollar om filmens Id finns eller inte
            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // POST: api/Movie
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            //Lägger till en ny film
            _context.Movies.Add(movie);
            //Sparar alla ändringar till databasen
            await _context.SaveChangesAsync();

            //Returnerar ett skapat objekt av filmen som skickades in
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }
    }
}
