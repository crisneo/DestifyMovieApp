using Destify.MovieApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destify.MovieApp.DataAccess.Entities
{
    public class MovieRating : BaseEntity
    {
        public int MovieId { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
    }
}
