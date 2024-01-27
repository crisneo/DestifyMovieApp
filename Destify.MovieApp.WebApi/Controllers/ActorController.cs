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
    public class ActorController : MovieAppBaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ActorController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ActorController(IMapper mapper, ILogger<ActorController> logger, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ActorReadDto>> Get()
        {
            var actors = _unitOfWork.ActorRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<ActorReadDto>>(actors));
        }

        [HttpGet("{id}")]
        public ActionResult<ActorReadDto> Get(int id)
        {
            var actor = _unitOfWork.ActorRepository.GetById(id);
            return Ok(_mapper.Map<ActorReadDto>(actor));
        }

        [HttpPost]
        [ApiKey]
        public ActionResult<ActorReadDto> Post([FromBody] ActorPostDto dto)
        {
            try
            {
                var created = _unitOfWork.ActorRepository.Insert(_mapper.Map<Actor>(dto));
                _unitOfWork.SaveChanges();
                return Ok(_mapper.Map<ActorReadDto>(created));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [ApiKey]
        public ActionResult<ActorReadDto> Put([FromBody] ActorPutDto dto)
        {
            try
            {
                var updated = _unitOfWork.ActorRepository.Update(_mapper.Map<Actor>(dto));
                _unitOfWork.SaveChanges();
                return Ok(_mapper.Map<ActorReadDto>(updated));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _unitOfWork.ActorRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public ActionResult<IEnumerable<ActorReadDto>> SearchByTitle([FromQuery] ActorFilterParams parameters)
        {
            var result = _unitOfWork.ActorRepository.SelectAll(x => x.Name.Contains(parameters.Name));
            return Ok(_mapper.Map<IEnumerable<ActorReadDto>>(result));
        }

        [HttpGet]
        [Route("{id}/movies")]
        public ActionResult<IEnumerable<MovieReadDto>> GetMovies(int id)
        {
            var movieActors = _unitOfWork.MovieActorRepository.GetAll().Where(x => x.ActorId == id).Select(x => x.MovieId);
            var movies = _unitOfWork.MovieRepository.SelectAll(x => movieActors.Contains(x.Id));
            return Ok(_mapper.Map<IEnumerable<MovieReadDto>>(movies));

        }
    }
}
