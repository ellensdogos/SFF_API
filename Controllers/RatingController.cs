using System;
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
    public class RatingController : ControllerBase
    {
        private readonly SFFContext _context;

        public RatingController(SFFContext context)
        {
            _context = context;
        }

        // GET: api/Rating
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatings()
        {
            //Returnerar betyget och inkluderar vilken studio som satt betyg för vilken film
            return await _context.Ratings
                                .Include(m => m.Movie)
                                .Include(s => s.Studio)
                                .ToListAsync();
        }

        // POST: api/Rating
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating(Rating rating)
        {
            //Kollar om betyget är korrekt och anropar isåfall metoden för att skapa en ny rating
            if (rating.IsRatingCorrect(rating))
            {
                await rating.NewRating(rating, _context);
            }

            //Kastar en exception som förklarar vad som är fel
            else
            {
                throw new ArgumentException("Rating has to be between 0-5");
            }

            return rating;
        }
    }
}
