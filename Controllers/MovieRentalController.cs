using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SFF_API.Context;
using SFF_API.Models;

namespace SFF_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieRentalController : ControllerBase
    {
        private readonly SFFContext _context;

        public MovieRentalController(SFFContext context)
        {
            _context = context;
        }

        // POST: api/MovieRental
        [HttpPost]
        public async Task<ActionResult<MovieRental>> NewMovieRental(MovieRental movieRental)
        {
            return Ok(await movieRental.RentMovie(_context, movieRental));
        }

        // GET: api/MovieRental
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieRental>>> GetMovieRentals()
        {
            return await _context.MovieRentals
                .Include(m => m.Movie)
                .Include(s => s.Studio)
                .ToListAsync();
        }

        //GET: api/MovieRental/{id}.{format}
        //Example: /api/movieRental/1.xml
        [HttpGet("{id}.{format?}"), FormatFilter]
        public async Task<ActionResult<Ticket>> GetTicketByRentalId(long Id)
        {
            var movieRental = await _context.MovieRentals
                .Include(m => m.Movie)
                .Include(s => s.Studio)
                .FirstAsync();

            var ticket = new Ticket();
            ticket.MovieTitle = movieRental.Movie.Title;
            ticket.Location = movieRental.Studio.Location;
            ticket.Date = DateTime.Now;

            return ticket;
        }

        //GET: api/MovieRental/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<MovieRental>>> GetRentedMoviesByStudio(long Id)
        {
            var rentedMovies = await _context.MovieRentals
                 .Where(x => x.StudioId == Id)
                 .Include(m => m.Movie)
                 .Include(s => s.Studio)
                 .ToListAsync();

            if (rentedMovies == null)
            {
                NotFound();
            }

            return rentedMovies;
        }

        // DELETE: api/MovieRental/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieRental>> ReturnMovie(long Id)
        {
            var movieRental = await _context.MovieRentals.FindAsync(Id);
            var movie = await _context.Movies.FindAsync(movieRental.MovieId);

            movie.IncreaseAmount(movie);

            _context.MovieRentals.Remove(movieRental);
            await _context.SaveChangesAsync();

            return movieRental;
        }
    }
}
