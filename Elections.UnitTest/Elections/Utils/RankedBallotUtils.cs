using Elections.Domain.Interfaces;
using static Elections.Application.Ballots.RankedBallotFactory;
using static Elections.Domain.Models.Candidates;
using static Elections.Domain.Models.Voters;

namespace Elections.UnitTest.Elections.Utils
{
    public class RankedBallotUtils : IElectionBallotUtils<IRankedBallot>
    {
        public List<ICandidate> Candidates { get; set; }

        public List<IVoter> Voters { get; set; }

        public RankedBallotUtils()
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

        public IReadOnlyList<IRankedBallot> GenerateBallotsWithWinner()
        {
            return new List<IRankedBallot>()
            {
                new RankedChoiceBallot(Voters[0], new List<IRankedVote>() {
                    new RankedChoiceVote(Candidates[0], 1), // Voter 1001, vote to John Snow
                    new RankedChoiceVote(Candidates[1], 1), // Voter 1001, vote to Daenerys Targaryen
                }),
                new RankedChoiceBallot(Voters[1], new List<IRankedVote>() {
                    new RankedChoiceVote(Candidates[0], 1), // Voter 1002, vote to John Snow
                    new RankedChoiceVote(Candidates[1], 1), // Voter 1002, vote to Daenerys Targaryen
                }),
                new RankedChoiceBallot(Voters[2], new List<IRankedVote>() {
                    new RankedChoiceVote(Candidates[0], 1), // Voter 1003, vote to John Snow
                    new RankedChoiceVote(Candidates[1], 1), // Voter 1003, vote to Daenerys Targaryen
                }),
                new RankedChoiceBallot(Voters[3], new List<IRankedVote>() {
                    new RankedChoiceVote(Candidates[1], 1), // Voter 1004, vote to Daenerys Targaryen
                    new RankedChoiceVote(Candidates[0], 2), // Voter 1004, vote to John Snow
                }),
                new RankedChoiceBallot(Voters[4], new List<IRankedVote>() {
                    new RankedChoiceVote(Candidates[1], 1), // Voter 1005, vote to Daenerys Targaryen
                    new RankedChoiceVote(Candidates[0], 2), // Voter 1005, vote to John Snow
                }),
                new RankedChoiceBallot(Voters[5], new List<IRankedVote>() {
                    new RankedChoiceVote(Candidates[1], 1), // Voter 1006, vote to Daenerys Targaryen
                    new RankedChoiceVote(Candidates[0], 2), // Voter 1006, vote to John Snow
                    new RankedChoiceVote(Candidates[2], 3), // Voter 1006, vote to Tyrion Lannister
                }),
                new RankedChoiceBallot(Voters[6], new List<IRankedVote>() {
                    new RankedChoiceVote(Candidates[2], 1), // Voter 1006, vote to Tyrion Lannister
                    new RankedChoiceVote(Candidates[0], 2), // Voter 1005, vote to John Snow
                }),
            };
        }

        public IReadOnlyList<IRankedBallot> GenerateEmptyBallots() => new List<IRankedBallot>();

        public IReadOnlyList<IRankedBallot> GenerateTiedBallots()
        {
            return new List<IRankedBallot>()
            {
                new RankedChoiceBallot(Voters[0], new List<IRankedVote>() {
                    new RankedChoiceVote(Candidates[0], 1), // Voter 1001, vote to John Snow
                    new RankedChoiceVote(Candidates[1], 1), // Voter 1001, vote to Daenerys Targaryen
                }),
                new RankedChoiceBallot(Voters[1], new List<IRankedVote>() {
                    new RankedChoiceVote(Candidates[0], 1), // Voter 1002, vote to John Snow
                    new RankedChoiceVote(Candidates[1], 1), // Voter 1002, vote to Daenerys Targaryen
                }),
                new RankedChoiceBallot(Voters[2], new List<IRankedVote>() {
                    new RankedChoiceVote(Candidates[0], 1), // Voter 1003, vote to John Snow
                    new RankedChoiceVote(Candidates[1], 1), // Voter 1003, vote to Daenerys Targaryen
                }),
                new RankedChoiceBallot(Voters[3], new List<IRankedVote>() {
                    new RankedChoiceVote(Candidates[1], 1), // Voter 1004, vote to Daenerys Targaryen
                    new RankedChoiceVote(Candidates[0], 2), // Voter 1004, vote to John Snow
                }),
                new RankedChoiceBallot(Voters[4], new List<IRankedVote>() {
                    new RankedChoiceVote(Candidates[1], 1), // Voter 1005, vote to Daenerys Targaryen
                    new RankedChoiceVote(Candidates[0], 2), // Voter 1005, vote to John Snow
                }),
                new RankedChoiceBallot(Voters[5], new List<IRankedVote>() {
                    new RankedChoiceVote(Candidates[1], 1), // Voter 1006, vote to Daenerys Targaryen
                    new RankedChoiceVote(Candidates[0], 2), // Voter 1006, vote to John Snow
                    new RankedChoiceVote(Candidates[2], 3), // Voter 1006, vote to Tyrion Lannister
                }),
            };
        }
    }
}
