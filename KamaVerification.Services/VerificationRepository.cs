namespace KamaVerification.Services
{
    public interface IVerificationRepository
    {
        string GenerateCode(int length = 6);
    }

    public class VerificationRepository : IVerificationRepository
    {
        private Random _random = new Random();
        private const string _digits = "0123456789";

        public string GenerateCode(int length = 6)
        {
            return new string(Enumerable.Repeat(_digits, length)
                .Select(s => s[_random.Next(length)]).ToArray());
        }
    }
}