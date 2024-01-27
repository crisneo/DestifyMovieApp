using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destify.MovieApp.Core.Entities
{
    public interface ISoftDeleteEntity : IEntity
    {
        public DateTime? DeletedAt { get; set; }
    }
}
