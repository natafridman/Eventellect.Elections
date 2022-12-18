using Elections.Exceptions;
using System.Net;

namespace Elections.Elections.Exceptions
{
    public class NoWinnerElectionException : DomainException
    {
        public override string Code => "no_winner_in_the_election";
        public override HttpStatusCode HttpStatusCode => HttpStatusCode.InternalServerError;
        public NoWinnerElectionException(string message) : base(message)
        {
        }
    }
}
