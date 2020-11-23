using GuessNumberGame.Domian.Entities;
using System.Collections.Generic;

namespace GuessNumberGame.DataRepository.Repositories
{
    public interface IScoresRepository
    {
        IList<PlayerScore> Get();
        int? TryAdd(PlayerScore score);
    }
}