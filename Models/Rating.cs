using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFF_API.Context;

namespace SFF_API.Models
{
    public class Rating
    {
        public long Id { get; set; }

        public int Rate { get; set; }

        public long StudioId { get; set; }

        public long MovieId { get; set; }

        public Movie Movie { get; set; }

        public Studio Studio { get; set; }

        //Kollar om betyget som skickas in är mellan 0-5
        public bool IsRatingCorrect(Rating rating)
        {
            if (rating.Rate <= 5 && rating.Rate >= 0)
            {
                return true;
            }

            return false;
        }

        //Skapar en ny rating på en film
        public async Task<ActionResult<Rating>> NewRating(Rating rating, SFFContext _context)
        {
            var movie = await _context.Movies.FindAsync(rating.MovieId);
            var studio = await _context.Studios.FindAsync(rating.StudioId);

            rating.MovieId = movie.Id;
            rating.StudioId = studio.Id;

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return rating;
        }
    }
}