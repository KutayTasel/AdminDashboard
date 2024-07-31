using System;

namespace GameDashboardProject.Application.Features.Queries.Login
{
    public record LoginQueryResponse
    (
        string Token,
        DateTime Expiration,
        bool Is,
        string Message
    );
}
