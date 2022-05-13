using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace KamaVerification.Data.Extensions
{
    public static partial class KamaVerificationOptionsExtensions
    {
        public static Guid? ToNullableGuid(this string s)
        {
            Guid i;

            return Guid.TryParse(s, out i)
                ? i
                : null;
        }

        /*
         * With Levenshtein distance, we measure similarity with fuzzy logic. 
         * This returns the number of character edits that must occur to get from string A to string B.
         * Source: https://www.dotnetperls.com/levenshtein
         */
        public static int DamerauLevenshteinDistance(this string s, string t)
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