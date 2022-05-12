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
            var distance = DamerauLevenshteinDistance(givenCode, expectedCode);

            if (closeness == distance)
            {
                var closenessWeight = closeness / (expectedCode.Length);

                return closeness - closenessWeight;
            }

            return closeness - (closeness / distance);
        }

        /*
         * With Levenshtein distance, we measure similarity with fuzzy logic. 
         * This returns the number of character edits that must occur to get from string A to string B.
         * Source: https://www.dotnetperls.com/levenshtein
         */
        private int DamerauLevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Verify arguments.
            if (n == 0) return m;
            if (m == 0) return n;

            // Initialize arrays
            for (int i = 0; i <= n; d[i, 0] = i++) { }
            for (int j = 0; j <= m; d[0, j] = j++) { }

            // Begin looping
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    // Compute cost
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            // Return cost
            return d[n, m];
        }
    }
}