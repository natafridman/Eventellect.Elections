using System.Collections.Generic;

namespace Elections.Domain.Interfaces;

public interface IBallot
{
    IVoter Voter { get; }
}
