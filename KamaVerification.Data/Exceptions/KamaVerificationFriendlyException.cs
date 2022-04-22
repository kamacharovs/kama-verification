using System.Net;

namespace KamaVerification.Data.Exceptions
{
    public class KamaVerificationFriendlyException : KamaVerificationException
    {
        public KamaVerificationFriendlyException()
        { }

        public KamaVerificationFriendlyException(string message)
            : base(message)
        { }

        public KamaVerificationFriendlyException(HttpStatusCode statusCode, string message)
            : base(statusCode, message)
        { }

        public KamaVerificationFriendlyException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
