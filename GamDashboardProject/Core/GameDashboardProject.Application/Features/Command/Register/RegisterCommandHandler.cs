using System;
using System.Threading;
using System.Threading.Tasks;
using GameDashboardProject.Application.Constants;
using GameDashboardProject.Domain.Identities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GameDashboardProject.Application.Features.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RegisterCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUserByName = await _userManager.FindByNameAsync(request.UserName);
                if (existingUserByName != null)
                {
                    throw new Exception(Messages.DuplicateUserName);
                }

                var existingUserByEmail = await _userManager.FindByEmailAsync(request.Email);
                if (existingUserByEmail != null)
                {
                    throw new Exception(Messages.DuplicateEmail);
                }
                var appUser = new AppUser
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    NormalizedUserName = request.UserName.ToUpperInvariant(),
                    NormalizedEmail = request.Email.ToUpperInvariant()
                };

                var result = await _userManager.CreateAsync(appUser, request.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors);
                    throw new Exception(Messages.RegistrationFailed + " " + errors);
                }

                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    var roleResult = await _roleManager.CreateAsync(new AppRole { Name = "Admin", NormalizedName = "ADMIN" });
                    if (!roleResult.Succeeded)
                    {
                        var roleErrors = string.Join(", ", roleResult.Errors);
                        throw new Exception(Messages.ActionFailed + " " + roleErrors);
                    }
                }

                var roleAssignmentResult = await _userManager.AddToRoleAsync(appUser, "Admin");
                if (!roleAssignmentResult.Succeeded)
                {
                    var roleAssignmentErrors = string.Join(", ", roleAssignmentResult.Errors);
                    throw new Exception(Messages.ActionFailed + " " + roleAssignmentErrors);
                }

                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
