using GameDashboardProject.Domain.Buildings;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboardProject.Domain.Identities
{
    public class AppUser:IdentityUser<Guid>
    {
    }
}
