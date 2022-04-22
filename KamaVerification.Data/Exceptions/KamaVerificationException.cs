using System.Net;
using System.Text.Json;

namespace KamaVerification.Data.Exceptions
{
    public abstract class KamaVerificationException : ApplicationException
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; }

        protected KamaVerificationException()
        { }

        protected KamaVerificationException(int statusCode)
        {
            StatusCode = statusCode;
        }

        protected KamaVerificationException(string message)
            : base(message)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        protected KamaVerificationException(string message, Exception inner)
            : base(message, inner)
        { }

        protected KamaVerificationException(int statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        protected KamaVerificationException(HttpStatusCode statusCode)
        {
            StatusCode = (int)statusCode;
        }

        protected KamaVerificationException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = (int)statusCode;
        }

        protected KamaVerificationException(int statusCode, Exception inner)
            : this(statusCode, inner.ToString())
        { }

        protected KamaVerificationException(HttpStatusCode statusCode, Exception inner)
            : this(statusCode, inner.ToString())
        { }

        protected KamaVerificationException(int statusCode, JsonElement errorObject)
            : this(statusCode, errorObject.ToString())
        {
            ContentType = @"application/problem+json";
        }
    }
}
