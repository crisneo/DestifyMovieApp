using Destify.MovieApp.Core.Repository;
using Destify.MovieApp.DataAccess.Context;
using Destify.MovieApp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destify.MovieApp.DataAccess.Repositories
{
    public class ActorRepository : GenericRepository<Actor>
    {
        public ActorRepository(MovieAppDbContext _context) : base(_context)
        {
        }
    }
}
