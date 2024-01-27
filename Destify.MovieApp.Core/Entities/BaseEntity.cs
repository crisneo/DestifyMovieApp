using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destify.MovieApp.Core.Entities
{
    public class BaseEntity : IAuditEntity
    {
        public DateTime LastModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int Id { get; set; }
    }
}
