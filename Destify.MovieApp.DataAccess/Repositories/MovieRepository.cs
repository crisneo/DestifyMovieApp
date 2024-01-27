using Destify.MovieApp.Core.Repository;
using Destify.MovieApp.DataAccess.Context;
using Destify.MovieApp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Destify.MovieApp.DataAccess.Repositories
{
    public class MovieRepository : GenericRepository<Movie>
    {
        public MovieRepository(MovieAppDbContext _context) : base(_context)
        {
        }
    }
}
