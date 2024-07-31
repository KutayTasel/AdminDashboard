using GameDashboardProject.Domain.Identities;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameDashboardProject.Application.Abstractions.Services
{
    public interface ITokenServices
    {
        Task<JwtSecurityToken> CreateToken(AppUser user, IList<string> roles);
        ClaimsPrincipal? GetClaimsPrincipalFromExpiredToken(string token);
    }
}
