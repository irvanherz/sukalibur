namespace Sukalibur.Shared.Options
{
    public class JwtAuthOptions
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string Secret { get; set; } = null!;
        public long AccessTokenTtl { get; set; }
        public long RefreshTokenTtl { get; set; }
    }
}
