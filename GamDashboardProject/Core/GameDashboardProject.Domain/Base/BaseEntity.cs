using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboardProject.Domain.Base
{
    public class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DataStatus Status { get; set; } 
    }
}
