using MediatR;
using GameDashboardProject.Application.Abstractions.Services;
using GameDashboardProject.Domain.Identities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using GameDashboardProject.Application.Features.Queries.Login;
using GameDashboardProject.Application.Constants;
using FluentValidation;
using System.Linq;

namespace GameDashboardProject.Application.Features.Handlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQueryRequest, LoginQueryResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenServices _tokenServices;
        private readonly IValidator<LoginQueryRequest> _validator;

        public LoginQueryHandler(UserManager<AppUser> userManager, ITokenServices tokenServices, IValidator<LoginQueryRequest> validator)
        {
            _userManager = userManager;
            _tokenServices = tokenServices;
            _validator = validator;
        }

        public async Task<LoginQueryResponse> Handle(LoginQueryRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new LoginQueryResponse(null, DateTime.MinValue, false, errorMessages);
            }
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new LoginQueryResponse(null, DateTime.MinValue, false, Messages.InvalidUsernameOrPassword);
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                return new LoginQueryResponse(null, DateTime.MinValue, false, Messages.InvalidUsernameOrPassword);
            }
            var roles = await _userManager.GetRolesAsync(user);
            var token = await _tokenServices.CreateToken(user, roles);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginQueryResponse(tokenString, token.ValidTo, true, null);
        }
    }
}
