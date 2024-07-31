namespace GameDashboardProject.Infrastructure.TokenService
{
    public class TokenSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecurityKey { get; set; }
        public int Expiration { get; set; }
    }
}
