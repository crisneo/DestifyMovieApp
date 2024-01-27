using AutoMapper;
using Destify.MovieApp.Core.Repository;
using Destify.MovieApp.DataAccess.Entities;
using Destify.MovieApp.DataAccess.UnitOfWork;
using Destify.MovieApp.WebApi.Controllers;
using Destify.MovieApp.WebApi.Dto;
using Destify.MovieApp.WebApi.MappingProfiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;


namespace Destify.MovieApp.Tests.Controllers
{
    public class ActorControllerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<ActorController>> _logger;
        private readonly Mock<IRepository<Actor>> _repository;

        public ActorControllerTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GeneralProfile>();
            }));
            _unitOfWork = new Mock<IUnitOfWork>();
            _logger = new Mock<ILogger<ActorController>>();
            _repository = new Mock<IRepository<Actor>>();
        }

        [Test]
        public void GetMoviesTest()
        {
            _repository.Setup(x => x.GetAll()).Returns(new List<Actor>());
            _unitOfWork.Setup(x => x.ActorRepository).Returns(_repository.Object);
            var ActorController = new ActorController(_mapper, _logger.Object, _unitOfWork.Object);
            var res = ActorController.Get();
            _repository.Verify(x => x.GetAll(), Times.Once());
            Assert.IsInstanceOf(typeof(ActionResult<IEnumerable<ActorReadDto>>), res);
        }

        [Test]
        public void PostMovieTest()
        {
            _repository.Setup(x => x.Insert(It.IsAny<Actor>())).Returns(new Actor());
            _unitOfWork.Setup(x => x.ActorRepository).Returns(_repository.Object);
            var actorController = new ActorController(_mapper, _logger.Object, _unitOfWork.Object);
            var res = actorController.Post(new ActorPostDto());
            _repository.Verify(x => x.Insert(It.IsAny<Actor>()), Times.Once());
            Assert.IsInstanceOf(typeof(ActionResult<ActorReadDto>), res);
        }

        [Test]
        public void PostMovieThrowErrorTest()
        {
            _repository.Setup(x => x.Insert(It.IsAny<Actor>())).Throws(() => new ArgumentException());
            _unitOfWork.Setup(x => x.ActorRepository).Returns(_repository.Object);
            _logger.Setup(x => x.Log(
              LogLevel.Error,
              It.IsAny<EventId>(),
              It.IsAny<It.IsAnyType>(),
              It.IsAny<Exception>(),
              It.IsAny<Func<It.IsAnyType, Exception?, string>>()
                )
           );
            var actorController = new ActorController(_mapper, _logger.Object, _unitOfWork.Object);
            var res = actorController.Post(new ActorPostDto());
            _repository.Verify(x => x.Insert(It.IsAny<Actor>()), Times.AtLeastOnce);
            _logger.Verify(
                   m => m.Log(
                       LogLevel.Error,
                       It.IsAny<EventId>(),
                       It.IsAny<It.IsAnyType>(),
                       It.IsAny<Exception>(),
                       It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                   Times.Once,
                   "Expected no errors would be written to ILogger, but found some"
               );
            Assert.IsInstanceOf(typeof(ProblemDetails), (res.Result as ObjectResult).Value);
        }
    }
}
