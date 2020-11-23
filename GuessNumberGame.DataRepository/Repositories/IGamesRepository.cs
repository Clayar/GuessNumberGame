using GuessNumberGame.Domian.Entities;

namespace GuessNumberGame.DataRepository.Repositories
{
    public interface IGamesRepository
    {
        Game Add(Game game);
        Game Edit(Game game);
        Game Get(string id);
    }
}