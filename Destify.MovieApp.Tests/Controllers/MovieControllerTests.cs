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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destify.MovieApp.Tests.Controllers
{
    public class MovieControllerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<MovieController>> _logger;
        private readonly Mock<IRepository<Movie>> _repository;

        public MovieControllerTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GeneralProfile>();
            }));
            _unitOfWork = new Mock<IUnitOfWork>();
            _logger = new Mock<ILogger<MovieController>>();
            _repository = new Mock<IRepository<Movie>>();
        }

        [Test]
        public void GetMoviesTest()
        {
            _repository.Setup(x => x.GetAll()).Returns(new List<Movie>());
            _unitOfWork.Setup(x => x.MovieRepository).Returns(_repository.Object);
            var movieController = new MovieController(_mapper, _logger.Object, _unitOfWork.Object);
            var res = movieController.Get();
            _repository.Verify(x => x.GetAll(), Times.Once());
            Assert.IsInstanceOf(typeof(ActionResult<IEnumerable<MovieReadDto>>), res);
        }

        [Test]
        public void PostMovieTest()
        {
            _repository.Setup(x => x.Insert(It.IsAny<Movie>())).Returns(new Movie());
            _unitOfWork.Setup(x => x.MovieRepository).Returns(_repository.Object);
            var movieController = new MovieController(_mapper, _logger.Object, _unitOfWork.Object);
            var res = movieController.Post(new MoviePostDto());
            _repository.Verify(x => x.Insert(It.IsAny<Movie>()), Times.Once());
            Assert.IsInstanceOf(typeof(ActionResult<MovieReadDto>), res);
        }

        [Test]
        public void PostMovieThrowErrorTest()
        {
            _repository.Setup(x => x.Insert(It.IsAny<Movie>())).Throws(() => new ArgumentException());
            _unitOfWork.Setup(x => x.MovieRepository).Returns(_repository.Object);
            _logger.Setup(x => x.Log(
              LogLevel.Error,
              It.IsAny<EventId>(),
              It.IsAny<It.IsAnyType>(),
              It.IsAny<Exception>(),
              It.IsAny<Func<It.IsAnyType, Exception?, string>>()
                )
           );
            var movieController = new MovieController(_mapper, _logger.Object, _unitOfWork.Object);
            var res = movieController.Post(new MoviePostDto());
            _repository.Verify(x => x.Insert(It.IsAny<Movie>()), Times.AtLeastOnce);
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
