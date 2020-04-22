using System;

namespace SFF_API.Models
{
    public class Trivia
    {
        public long Id { get; set; }

        public string TriviaText { get; set; }

        public long MovieId { get; set; }

        public Movie Movie { get; set; }

    }
}
