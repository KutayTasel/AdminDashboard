using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboardProject.Domain.Base
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        DateTime? DeletedAt { get; set; }
        DataStatus Status { get; set; }
    }
}
