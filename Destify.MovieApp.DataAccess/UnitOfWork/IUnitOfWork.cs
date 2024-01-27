using Destify.MovieApp.Core.Repository;
using Destify.MovieApp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destify.MovieApp.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Movie> MovieRepository { get; }

        IRepository<Actor> ActorRepository { get; }
        IRepository<MovieActor> MovieActorRepository { get; }
        void SaveChanges();
    }
}
