using Destify.MovieApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destify.MovieApp.DataAccess.Entities
{
    public class MovieActor : BaseEntity
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
    }
}
