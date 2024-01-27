using Destify.MovieApp.DataAccess.Context;
using Destify.MovieApp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destify.MovieApp.DataAccess.Repositories
{
    public class MovieActorRepository : GenericRepository<MovieActor>
    {
        public MovieActorRepository(MovieAppDbContext _context) : base(_context)
        {
        }
    }
}
