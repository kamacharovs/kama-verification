using System.Net;

namespace KamaVerification.Data.Exceptions
{
    internal class KamaVerificationNotFoundException : KamaVerificationFriendlyException
    {
        private const string DefaultMessage = "The requested item was not found.";

        public KamaVerificationNotFoundException()
            : base(HttpStatusCode.NotFound, DefaultMessage)
        { }

        public KamaVerificationNotFoundException(string message)
            : base(HttpStatusCode.NotFound, message)
        { }

        public KamaVerificationNotFoundException(string message, Exception inner)
            : base(message, inner)
        { }

        public KamaVerificationNotFoundException(Exception inner)
            : base(DefaultMessage, inner)
        { }
    }
}
