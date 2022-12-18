using Elections.Domain.Interfaces;
using Elections.Elections.Exceptions;
using Elections.Elections.Services;
using Elections.UnitTest.Elections.Utils;
using static Elections.Domain.Models.Candidates;

namespace Elections.UnitTest
{
    public class RankedChoiceElectionServiceTests
    {
        private readonly static RankedChoiceElectionService rankedChoiceElectionService = new();
        private readonly static RankedBallotUtils rankedBallotUtils = new();
        private static List<ICandidate> Candidates => rankedBallotUtils.Candidates;

        [Fact]
        public void Run_WithOneCandidate_TheOnlyCandidateWins()
        {
            /* 
             * If there is only one candidate in the election, he or she must be the winner 
             */

            // Arrange 
            var oneCandidate = Candidates.Take(1).ToList();
            var ballots = rankedBallotUtils.GenerateBallotsWithWinner();

            // Act
            var electionWinner = rankedChoiceElectionService.Run(ballots, oneCandidate);

            // Assert
            Assert.True(electionWinner.Id == oneCandidate.First().Id);
        }

        [Fact]
        public void Run_WithNoBallots_ThrowsEmptyElectionException()
        {
            /* 
             * If there is no ballots in the election then is an empty election
             */

            // Arrange 
            var ballots = rankedBallotUtils.GenerateEmptyBallots();

            // Act & Assert
            Assert.Throws<EmptyElectionException>(() =>
            {
                rankedChoiceElectionService.Run(ballots, Candidates);
            });
        }

        [Fact]
        public void Run_WithNoCandidates_ThrowsEmptyElectionException()
        {
            /* 
             * If there is no candidates in the election then is an empty election
             */

            // Arrange 
            var emptyCandidates = new List<ICandidate>();
            var ballots = rankedBallotUtils.GenerateBallotsWithWinner();

            // Act & Assert
            Assert.Throws<EmptyElectionException>(() =>
            {
                rankedChoiceElectionService.Run(ballots, emptyCandidates);
            });
        }

        [Fact]
        public void Run_TwoCandidatesWithTheSameCountVotes_ThrowsTieElectionException()
        {
            /* 
             *  candidate[0] is John Snow
             *  candidate[1] is Daenerys Targaryen
             *  Both have the same count of votes, therefore is a tie
             */

            // Arrange 
            var ballots = rankedBallotUtils.GenerateTiedBallots();

            // Act & Assert
            Assert.Throws<TieElectionException>(() =>
            {
                rankedChoiceElectionService.Run(ballots, Candidates);
            });
        }

        [Fact]
        public void Run_WithCandidateJohnSnowAsMostVoted_CandidateJohnSnowWins()
        {
            /* 
             *  candidate[0] is John Snow
             *  He is the one with most votes, therefore he is the winner 
             */

            // Arrange 
            var ballots = rankedBallotUtils.GenerateBallotsWithWinner();

            // Act
            var electionWinner = rankedChoiceElectionService.Run(ballots, Candidates);

            // Assert
            Assert.True(electionWinner.Id == Candidates[0].Id);
        }

        [Fact]
        public void Run_WithVotesToNonAvailableCandidates_ThrowsNoWinnerElectionException()
        {
            /* 
                All the ballots contains votes that are not referenced to any available candidates, therefore there is no winner
             */

            // Arrange
            var someoneElseCandidate = new List<ICandidate>() 
            { 
                new Candidate(201, "Someone Else 1")
            };
            var ballots = rankedBallotUtils.GenerateBallotsWithWinner();

            // Act & Assert
            Assert.Throws<NoWinnerElectionException>(() =>
            {
                rankedChoiceElectionService.Run(ballots, someoneElseCandidate);
            });
        }
    }
}