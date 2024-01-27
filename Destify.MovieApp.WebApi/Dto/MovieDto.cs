using System.ComponentModel.DataAnnotations;

namespace Destify.MovieApp.WebApi.Dto
{
    public class MovieDto
    {

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

    }

    public class MovieReadDto : MovieDto
    {
        public int Id { get; set; }
        public IEnumerable<MovieRatingReadDto> Ratings { get; set; }

    }

    public class MoviePostDto : MovieDto
    {
    }

    public class MoviePutDto : MovieDto
    {
        public int Id { get; set; }
    }
}
