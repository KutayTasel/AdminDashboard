using GameDashboardProject.Application.Abstractions.Services;
using GameDashboardProject.Domain.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboardProject.Infrastructure.TokenService
{
    public class TokenServices : ITokenServices
    {
        private readonly TokenSettings _tokenSettings;
        private readonly UserManager<AppUser> _userManager;

        public TokenServices(IOptions<TokenSettings> tokenSettings, UserManager<AppUser> userManager)
        {
            _tokenSettings = tokenSettings.Value;
            _userManager = userManager;
        }

        public async Task<JwtSecurityToken> CreateToken(AppUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecurityKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                audience: _tokenSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_tokenSettings.Expiration),
                signingCredentials: signingCredentials
            );

            return token;
        }

        public ClaimsPrincipal? GetClaimsPrincipalFromExpiredToken(string token)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecurityKey)),
            };

            var handler = new JwtSecurityTokenHandler();
            var principal = handler.ValidateToken(token, validationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token.");
            }

            return principal;
        }
    }
}
