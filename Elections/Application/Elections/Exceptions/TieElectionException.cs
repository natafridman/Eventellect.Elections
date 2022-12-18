using Elections.Exceptions;
using System.Net;

namespace Elections.Elections.Exceptions
{
    public class TieElectionException : DomainException
    {
        public override string Code => "tie_election_between_first_and_second_candidates";
        public override HttpStatusCode HttpStatusCode => HttpStatusCode.Ambiguous;
        public TieElectionException(string message) : base(message)
        {
        }
    }
}
