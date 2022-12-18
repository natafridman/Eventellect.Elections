using Elections.Domain.Interfaces;
using Elections.Elections.Exceptions;
using Elections.Extensions;

namespace Elections.Elections.Services;

public class PluralityElectionService : IElection<ISingleVoteBallot>
{
    public ICandidate Run(IReadOnlyList<ISingleVoteBallot> ballots, IReadOnlyList<ICandidate> candidates)
    {
        /*
            Plurality Election Algorithm
            - The candidate with the most votes wins.
            - There is no minimum vote threshold.
        */

        // If there is no ballots, there is nothing to count, so throw an exception
        if (ballots == null || ballots.Count == 0)
            throw new EmptyElectionException("The election can not be run because there is no ballots to count");

        // If there is no candidates, there is no one to win
        if (candidates == null || candidates.Count == 0)
            throw new EmptyElectionException("The election can not be run because there is no candidates to win");

        var candidatesOrderedByPositions = ballots
            .GroupBy(ballot => ballot.Vote.Candidate)
            .Join(candidates,
                  votes_pk => votes_pk.Key.Id,
                  candidate_pk => candidate_pk.Id,
                  (Votes, Candidate) => new { Votes = Votes.Count(), Candidate })
            .OrderByDescending(x => x.Votes);

        var winnerCandidate = candidatesOrderedByPositions.FirstOrDefault();
        if (winnerCandidate == null)
            throw new NoWinnerElectionException("There is no winner in the election");

        // Check if there is a tie with the second candidate
        var secondCandidate = candidatesOrderedByPositions.Second();
        if(winnerCandidate.Votes == secondCandidate?.Votes)
            throw new TieElectionException($"There is a tie between {winnerCandidate.Candidate.Name} and {secondCandidate.Candidate.Name}, no one wins");

        return winnerCandidate.Candidate;
    }
}
