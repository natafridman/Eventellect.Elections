using Elections.Domain.Interfaces;
using Elections.Elections.Exceptions;

namespace Elections.Elections.Services;

public class RankedChoiceElectionService : IElection<IRankedBallot>
{
    public ICandidate Run(IReadOnlyList<IRankedBallot> ballots, IReadOnlyList<ICandidate> candidates)
    {
        /*
            Ranked Choice Election Algorithm
            - If a candidate wins a majority of first-preference votes, he or she is declared the winner.
            - If not, the candidate with the fewest first-preference votes is eliminated.
            - First-preference votes cast for the failed candidate are eliminated, lifting the next-preference choices indicated on those ballots.
            - The process is repeated until a candidate wins an outright majority.
        */

        // If there is no ballots, there is nothing to count, so throw an exception
        if (ballots == null || ballots.Count == 0)
            throw new EmptyElectionException("The election can not be run because there is no ballots to count");

        // If there is no candidates, there is no one to win
        if (candidates == null || candidates.Count == 0)
            throw new EmptyElectionException("The election can not be run because there is no candidates to win");

        // Filter the ballots without the previous looser candidates
        var ballotsWithoutLooserCandidates = ballots
            .Select(ballot => new
            {
                Votes = ballot.Votes
                    .OrderBy(vote => vote.Rank)
                    .Join(candidates,
                        votes_pk => votes_pk.Candidate.Id,
                        candidate_pk => candidate_pk.Id,
                        (Votes, Candidate) => new { Votes, Candidate })
            })
            .Where(ballot => ballot.Votes.Any());

        var candidatesOrderedByPositions = ballotsWithoutLooserCandidates
            .GroupBy(ballot => ballot.Votes.First().Candidate)
            .Join(candidates,
                  votes_pk => votes_pk.Key.Id,
                  candidate_pk => candidate_pk.Id,
                  (Votes, Candidate) => new { Votes = Votes.Count(), Candidate })
            .OrderByDescending(x => x.Votes);

        var winnerCandidate = candidatesOrderedByPositions.FirstOrDefault();
        if (winnerCandidate == null)
            throw new NoWinnerElectionException("There is no winner in the election due the ballots are not the same as the available candidates");

        // If the candidate gets half the votes plus one, is the winner
        var minimumVotesToWin = Math.Floor((decimal)ballots.Count / 2) + 1;

        if (winnerCandidate.Votes >= minimumVotesToWin || candidatesOrderedByPositions.Count() == 1)
            return winnerCandidate.Candidate;

        var looserCandidate = candidatesOrderedByPositions.Last();

        if (candidatesOrderedByPositions.Count() == 2 && winnerCandidate.Votes == looserCandidate.Votes)
            throw new TieElectionException($"There is a tie between {winnerCandidate.Candidate.Name} and {looserCandidate.Candidate.Name}, no one wins");

        return Run(ballots, candidates.Where(candidate => candidate.Id != looserCandidate.Candidate.Id).ToList());
    }
}
