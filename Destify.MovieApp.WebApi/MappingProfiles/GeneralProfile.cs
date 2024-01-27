using AutoMapper;
using Destify.MovieApp.DataAccess.Entities;
using Destify.MovieApp.WebApi.Dto;

namespace Destify.MovieApp.WebApi.MappingProfiles
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Movie, MovieReadDto>();
            CreateMap<MoviePostDto, Movie>();
            CreateMap<MoviePutDto, Movie>();
            CreateMap<Actor, ActorReadDto>();
            CreateMap<ActorPostDto, Actor>();
            CreateMap<ActorPutDto, Actor>();
        }
    }
}
