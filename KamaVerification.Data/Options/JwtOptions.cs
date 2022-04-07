namespace KamaVerification.Data.Options
{
    public class JwtOptions
    {
        public const string Section = "Jwt";

        public int Expires { get; set; }
        public string Type { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }
}
