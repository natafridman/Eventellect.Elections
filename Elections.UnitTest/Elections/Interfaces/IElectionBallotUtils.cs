namespace Elections.Domain.Interfaces;

public interface IElectionBallotUtils<TBallot> where TBallot : IBallot
{
    protected List<ICandidate> Candidates { get; set; }
    protected List<IVoter> Voters { get; }
    protected IReadOnlyList<TBallot> GenerateTiedBallots();
    protected IReadOnlyList<TBallot> GenerateBallotsWithWinner();
    protected IReadOnlyList<TBallot> GenerateEmptyBallots();
}
