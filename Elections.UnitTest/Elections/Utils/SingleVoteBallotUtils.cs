using Elections.Domain.Interfaces;
using static Elections.Application.Ballots.SingleVoteBallotFactory;
using static Elections.Domain.Models.Candidates;
using static Elections.Domain.Models.Voters;

namespace Elections.UnitTest.Elections.Utils
{
    public class SingleVoteBallotUtils : IElectionBallotUtils<ISingleVoteBallot>
    {
        public List<ICandidate> Candidates { get; set; }

        public List<IVoter> Voters { get; set; }

        public SingleVoteBallotUtils()
        {
            Candidates = new()
            {
                new Candidate(1, "John Snow"),
                new Candidate(2, "Daenerys Targaryen"),
                new Candidate(3, "Tyrion Lannister"),
                new Candidate(4, "Sansa Stark")
            };

            Voters = new()
            {
                new Voter(1001, "Voter 1001"),
                new Voter(1002, "Voter 1002"),
                new Voter(1003, "Voter 1003"),
                new Voter(1004, "Voter 1004"),
                new Voter(1005, "Voter 1005"),
                new Voter(1006, "Voter 1006"),
                new Voter(1007, "Voter 1007")
            };
        }

        public IReadOnlyList<ISingleVoteBallot> GenerateBallotsWithWinner()
        {
            /* 
                Candidate           Votes
                John Snow           3
                Daenerys Targaryen  1
                Tyrion Lannister    1
                Sansa Stark         0
             */

            return new List<SimpleBallot>()
            {
                new SimpleBallot(Voters[0], new SimpleVote(Candidates[0])), // Candidate 1 vote to John Snow
                new SimpleBallot(Voters[1], new SimpleVote(Candidates[0])), // Candidate 2 vote to John Snow
                new SimpleBallot(Voters[2], new SimpleVote(Candidates[0])), // Candidate 3 vote to John Snow
                new SimpleBallot(Voters[3], new SimpleVote(Candidates[1])), // Candidate 4 vote to Daenerys Targaryen
                new SimpleBallot(Voters[4], new SimpleVote(Candidates[2])), // Candidate 5 vote to Tyrion Lannister
            };
        }

        public IReadOnlyList<ISingleVoteBallot> GenerateEmptyBallots() => new List<SimpleBallot>();

        public IReadOnlyList<ISingleVoteBallot> GenerateTiedBallots()
        {
            /* 
               Candidate           Votes
               John Snow           3
               Daenerys Targaryen  3
               Tyrion Lannister    0
               Sansa Stark         0
            */

            return new List<SimpleBallot>()
            {
                new SimpleBallot(Voters[0], new SimpleVote(Candidates[0])), // Candidate 1 vote to John Snow
                new SimpleBallot(Voters[1], new SimpleVote(Candidates[0])), // Candidate 2 vote to John Snow
                new SimpleBallot(Voters[2], new SimpleVote(Candidates[0])), // Candidate 3 vote to John Snow
                new SimpleBallot(Voters[3], new SimpleVote(Candidates[1])), // Candidate 4 vote to Daenerys Targaryen
                new SimpleBallot(Voters[4], new SimpleVote(Candidates[1])), // Candidate 5 vote to Daenerys Targaryen
                new SimpleBallot(Voters[5], new SimpleVote(Candidates[1])), // Candidate 6 vote to Daenerys Targaryen
            };
        }
    }
}
