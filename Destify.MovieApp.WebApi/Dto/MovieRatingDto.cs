namespace Destify.MovieApp.WebApi.Dto
{
    public class MovieRatingDto
    {
        public int Rating { get; set; }
        public DateTime Date { get; set; }
    }

    public class MovieRatingReadDto : MovieRatingDto
    {

    }

    public class MovieRatingPostDto : MovieRatingDto
    {

    }
}
