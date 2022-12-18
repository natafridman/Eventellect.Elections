using System.Diagnostics;
using Elections.Elections.Services;
using Elections.Domain.Models;
using Elections.Domain.Interfaces;
using Elections.Application.Ballots;

const int numVoters = 100_000;
var voters = Voters.Create(numVoters, Candidates.Official);

RunSimpleElection(voters);

RunRankedChoiceElection(voters);

static void RunSimpleElection(IReadOnlyList<IVoter> voters)
{
    var ballots = SingleVoteBallotFactory.Create(voters, Candidates.Official);
    RunElection<PluralityElectionService, ISingleVoteBallot>(ballots);
}

static void RunRankedChoiceElection(IReadOnlyList<IVoter> voters)
{
    var ballots = RankedBallotFactory.Create(voters, Candidates.Official);
    RunElection<RankedChoiceElectionService, IRankedBallot>(ballots);
}

static void RunElection<TElection, TBallot>(IReadOnlyList<TBallot> ballots)
    where TElection : IElection<TBallot>, new()
    where TBallot : IBallot
{
    var stopwatch = Stopwatch.StartNew();
    Console.WriteLine($"========== {typeof(TElection).Name} ==========");
    Console.WriteLine();

    try
    {
        var election = new TElection();
        var winner = election.Run(ballots, Candidates.Official);
        Console.WriteLine(FormatMessage($"Winner is {winner?.Name}"));
    }
    catch (Exception ex)
    {
        Console.WriteLine(FormatMessage(ex.ToString()));
    }

    Console.WriteLine();
    Console.WriteLine($"============================================");
    Console.WriteLine();
    Console.WriteLine();

    string FormatMessage(string prefix)
        => $"{prefix} [{stopwatch!.Elapsed.TotalMilliseconds} ms]";
}