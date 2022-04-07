namespace KamaVerification.Data
{
    public static class Keys
    {
        public const string Jwt = nameof(Jwt);
        public const string Bearer = nameof(Bearer);
        public const string Expires = nameof(Expires);
        public const string Type = nameof(Type);
        public const string Issuer = nameof(Issuer);
        public const string Audience = nameof(Audience);
        public const string Key = nameof(Key);
        public const string JwtExpires = Jwt + ":" + Expires;
        public const string JwtType = Jwt + ":" + Type;
        public const string JwtIssuer = Jwt + ":" + Issuer;
        public const string JwtAudience = Jwt + ":" + Audience;
        public const string JwtKey = Jwt + ":" + Key;
    }
}