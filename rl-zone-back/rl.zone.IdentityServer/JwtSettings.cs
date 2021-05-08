namespace rl.zone.IdentityServer
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public int ExpiresIn { get; set; }
    }
}