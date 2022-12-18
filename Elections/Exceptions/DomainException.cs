using System.Net;

namespace Elections.Exceptions
{
    public abstract class DomainException : Exception
    {
        public abstract string Code { get; }
        public abstract HttpStatusCode HttpStatusCode { get; }

        protected DomainException(string? message) : base(message)
        {

        }
    }
}
