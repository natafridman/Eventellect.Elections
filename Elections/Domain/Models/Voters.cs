using Elections.Domain.Interfaces;

namespace Elections.Domain.Models;

public static class Voters
{
    public static IReadOnlyList<IVoter> Create(int voterCount, IReadOnlyList<ICandidate> candidates)
    {
        var totalVoters = voterCount + candidates.Count;
        var maxCandidateId = candidates.Max(c => c.Id);
        var voters = Enumerable.Range(maxCandidateId + 1, voterCount).Select(CreateVoter).ToList();
        voters.AddRange(candidates);
        return voters;
    }

    private static IVoter CreateVoter(int id)
    {
        return new Voter(id, $"Voter {id}");
    }

    public record Voter(int Id, string Name) : IVoter;
}
