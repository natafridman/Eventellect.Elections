using Elections.Exceptions;
using System.Net;

namespace Elections.Elections.Exceptions
{
    public class EmptyElectionException : DomainException
    {
        public override string Code => "no_ballots_or_candidates_in_the_election";
        public override HttpStatusCode HttpStatusCode => HttpStatusCode.NoContent;
        public EmptyElectionException(string message) : base(message)
        {
        }
    }
}
