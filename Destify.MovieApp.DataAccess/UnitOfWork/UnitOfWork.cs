using Destify.MovieApp.Core.Repository;
using Destify.MovieApp.DataAccess.Context;
using Destify.MovieApp.DataAccess.Entities;
using Destify.MovieApp.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destify.MovieApp.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieAppDbContext _context;
        public UnitOfWork(MovieAppDbContext context)
        {
            _context = context;
            MovieRepository = new MovieRepository(_context);
            ActorRepository = new ActorRepository(_context);
            MovieActorRepository = new MovieActorRepository(_context);
        }

        public IRepository<Movie> MovieRepository { get; private set; }

        public IRepository<Actor> ActorRepository { get; private set; }

        public IRepository<MovieActor> MovieActorRepository { get; private set; }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _context != null)
            {
                _context.Dispose();
            }
        }
    }
}
