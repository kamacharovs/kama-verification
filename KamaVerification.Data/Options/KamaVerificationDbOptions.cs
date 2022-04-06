namespace KamaVerification.Data.Options
{
    public class KamaVerificationDbOptions
    {
        public const string Section = "KamaVerificationDb";

        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }

        public string ConnectionString => $"Server={Host};Username={Username};Password={Password};Database={DatabaseName}";
    }
}
