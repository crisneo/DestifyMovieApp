using Destify.MovieApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destify.MovieApp.DataAccess.Entities
{
    public class Actor : BaseEntity
    {
        public string Name { get; set; }
        public string Country { get; set; }

    }
}
