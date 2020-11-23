using GuessNumberGame.Application.DTOModels;

namespace GuessNumberGame.Application.Services
{
    public interface IGameService
    {
        GameIdDTO AddGame(NewGameDTO newGame);
        HighScoreDTO GetHighScore();
        GameDTO TryGuesst(GuessDTO guess);
    }
}