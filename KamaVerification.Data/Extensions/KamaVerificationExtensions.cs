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
    }
}