using KamaVerification.Data;
using KamaVerification.Data.Models;
using KamaVerification.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KamaVerification.Services
{
    public interface IVerificationRepository
    {
        string GenerateCode(int length = 6);
        double CalculateDifference(string givenCode, string expectedCode);
    }

    public class VerificationRepository : IVerificationRepository
    {
        private readonly ILogger<VerificationRepository> _logger;

        private Random _random = new Random();
        private const string _digits = "0123456789";

        public VerificationRepository(
            ILogger<VerificationRepository> logger)
        {
            _logger = logger;
        }

        public string GenerateCode(int length = 6)
        {
            return new string(Enumerable.Repeat(_digits, length)
                .Select(s => s[_random.Next(length)]).ToArray());
        }
        
        public double CalculateDifference(string givenCode, string expectedCode)
        {
            var closeness = 1.0;
            var distance = givenCode.DamerauLevenshteinDistance(expectedCode);

            if (closeness == distance)
            {
                var closenessWeight = closeness / (expectedCode.Length);

                return closeness - closenessWeight;
            }

            return closeness - (closeness / distance);
        }
    }
}