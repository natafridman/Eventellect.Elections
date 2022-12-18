using Elections.Domain.Interfaces;
using Elections.Elections.Exceptions;
using Elections.Elections.Services;
using Elections.UnitTest.Elections.Utils;
using static Elections.Domain.Models.Candidates;

namespace Elections.UnitTest
{
    public class PluralityElectionServiceTests
    {
        private readonly static PluralityElectionService pluralityElectionService = new();
        private readonly static SingleVoteBallotUtils singleVoteBallotUtils = new();
        private static List<ICandidate> Candidates => singleVoteBallotUtils.Candidates;


        [Fact]
        public void Run_WithOneCandidate_TheOnlyCandidateWins()
        {
            /* 
             * If there is only one candidate in the election, he or she must be the winner 
             */

            // Arrange 
            var oneCandidate = Candidates.Take(1).ToList();
            var ballots = singleVoteBallotUtils.GenerateBallotsWithWinner();

            // Act
            var electionResult = pluralityElectionService.Run(ballots, oneCandidate);

            // Assert
            Assert.True(electionResult.Id == oneCandidate.First().Id);
        }

        [Fact]
        public void Run_WithNoBallots_ThrowsEmptyElectionException()
        {
            /* 
             * If there is no ballots in the election then is an empty election
             */

            // Arrange 
            var ballots = singleVoteBallotUtils.GenerateEmptyBallots();

            // Act & Assert
            Assert.Throws<EmptyElectionException>(() =>
            {
                pluralityElectionService.Run(ballots, Candidates);
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
            var ballots = singleVoteBallotUtils.GenerateBallotsWithWinner();

            // Act & Assert
            Assert.Throws<EmptyElectionException>(() =>
            {
                pluralityElectionService.Run(ballots, emptyCandidates);
            });
        }

        [Fact]
        public void Run_WithCandidateJohnSnowAsMostVoted_CandidateJohnSnowWins()
        {
            /* 
             *  With candidate John Snow as the most voted, he will be the winner of the election
             */

            // Arrange 
            var ballots = singleVoteBallotUtils.GenerateBallotsWithWinner();

            // Act
            var electionResult = pluralityElectionService.Run(ballots, Candidates);

            // Assert
            Assert.True(electionResult.Id == Candidates.First().Id);
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
            var ballots = singleVoteBallotUtils.GenerateBallotsWithWinner();

            // Act & Assert
            Assert.Throws<NoWinnerElectionException>(() =>
            {
                pluralityElectionService.Run(ballots, someoneElseCandidate);
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
            var ballots = singleVoteBallotUtils.GenerateTiedBallots();

            // Act & Assert
            Assert.Throws<TieElectionException>(() =>
            {
                pluralityElectionService.Run(ballots, Candidates);
            });
        }
    }
}