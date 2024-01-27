using AutoMapper;
using Destify.MovieApp.Core.Repository;
using Destify.MovieApp.DataAccess.Entities;
using Destify.MovieApp.DataAccess.UnitOfWork;
using Destify.MovieApp.WebApi.Authorization;
using Destify.MovieApp.WebApi.Dto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Destify.MovieApp.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MovieController : MovieAppBaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MovieController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public MovieController(IMapper mapper, ILogger<MovieController> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieReadDto>> Get()
        {
            var movies = _unitOfWork.MovieRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<MovieReadDto>>(movies));
        }

        [HttpGet("{id}")]
        public ActionResult<MovieReadDto> Get(int id)
        {
            var movie = _unitOfWork.MovieRepository.GetById(id);
            return Ok(_mapper.Map<MovieReadDto>(movie));
        }

        [HttpPost]
        [ApiKey]
        public ActionResult<MovieReadDto> Post([FromBody] MoviePostDto dto)
        {
            try
            {
                var created = _unitOfWork.MovieRepository.Insert(_mapper.Map<Movie>(dto));
                _unitOfWork.SaveChanges();
                return Ok(_mapper.Map<MovieReadDto>(created));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [ApiKey]
        public ActionResult<MovieReadDto> Put([FromBody] MoviePutDto dto)
        {
            try
            {
                var updated = _unitOfWork.MovieRepository.Update(_mapper.Map<Movie>(dto));
                _unitOfWork.SaveChanges();
                return Ok(_mapper.Map<MovieReadDto>(updated));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ApiKey]
        public ActionResult Delete(int id)
        {
            _unitOfWork.MovieRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public ActionResult<IEnumerable<MovieReadDto>> SearchByTitle([FromQuery] MovieFilterParams parameters)
        {
            var result = _unitOfWork.MovieRepository.SelectAll(x => x.Title.Contains(parameters.Title));
            return Ok(_mapper.Map<IEnumerable<MovieReadDto>>(result));
        }

        [HttpGet]
        [Route("{id}/actors")]
        public ActionResult<IEnumerable<ActorReadDto>> GetActors(int id)
        {
            var movieActors = _unitOfWork.MovieActorRepository.GetAll().Where(x => x.MovieId == id).Select(x => x.ActorId);
            var actors = _unitOfWork.ActorRepository.SelectAll(x => movieActors.Contains(x.Id));
            return Ok(_mapper.Map<IEnumerable<ActorReadDto>>(actors));

        }

        [HttpPost]
        [Route("{id}/actor/{actorId}")]
        public ActionResult PostActor(int id, int actorId)
        {
            _unitOfWork.MovieActorRepository.Insert(new MovieActor() { MovieId = id, ActorId = actorId });
            return Ok();
        }
    }
}
