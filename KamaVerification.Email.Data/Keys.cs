namespace KamaVerification.Email.Data
{
    public static class Keys
    {
        public static string Email = nameof(Email);
        public static string ApiKey = nameof(ApiKey);
        public static string From = nameof(From);
        public static string EmailApiKey = nameof(Email) + ":" + nameof(ApiKey);
        public static string EmailFrom = nameof(Email) + ":" + nameof(From);
    }
}