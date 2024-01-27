using System.ComponentModel.DataAnnotations;

namespace Destify.MovieApp.WebApi.Dto
{
    public class ActorDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }

    }

    public class ActorReadDto : ActorDto
    {
        public int Id { get; set; }
    }

    public class ActorPostDto : ActorDto
    {

    }

    public class ActorPutDto : ActorDto
    {
        public int Id { get; set; }
    }
}
