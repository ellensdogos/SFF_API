
namespace SFF_API.Models
{
    public class Movie
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public int MaxAmountToRent { get; set; }

        //Ökar antalet filmer som finns att låna ut
        public Movie IncreaseAmount(Movie movie)
        {
            movie.MaxAmountToRent++;
            return movie;
        }

        //Minskar antalet filmer som finns att låna ut
        public Movie ReduceAmount(Movie movie)
        {
            movie.MaxAmountToRent--;
            return movie;
        }

        //Kollar om filmen finns i lager för att hyra ut
        public bool MovieInStock(Movie movie)
        {
            if (movie.MaxAmountToRent > 0)
            {
                return true;
            }

            return false;
        }
    }
}
