using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFF_API.Context;

namespace SFF_API.Models
{
    public class MovieRental
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public long MovieId { get; set; }

        public long StudioId { get; set; }

        public Movie Movie { get; set; }

        public Studio Studio { get; set; }


        //Askynkron metod som skapar en uthyrning av en film. 
        public async Task<ActionResult<MovieRental>> RentMovie(SFFContext _context, MovieRental movieRental)
        {
            var movie = await _context.Movies.FindAsync(movieRental.MovieId);
            var studio = await _context.Studios.FindAsync(movieRental.StudioId);

            //Om filmen finns i lager 
            if (movie.MovieInStock(movie))
            {
                //Antalet kvar för uthyrning minskas
                movie.ReduceAmount(movie);

                //Filmen och studion kopplas samman i movieRental för uthyrning
                movieRental.MovieId = movie.Id;
                movieRental.StudioId = studio.Id;
                movieRental.Date = DateTime.Now;

                //En uthyrning läggs till och returneras
                _context.MovieRentals.Add(movieRental);
                await _context.SaveChangesAsync();

                return movieRental;
            }

            //Om den inte finns i lager returneras null
            else
            {
                return null;
            }
        }
    }
}
