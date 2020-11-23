using GuessNumberGame.Application.DTOModels;
using GuessNumberGame.Application.Infrastructure;
using GuessNumberGame.DataRepository.Repositories;
using GuessNumberGame.Domian.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuessNumberGame.Application.Services
{
    public class GameService : IGameService
    {
        private IGamesRepository gamesRepository;
        private IScoresRepository scoresRepository;

        public GameService(IGamesRepository _gamesRepository, IScoresRepository _scoresRepository)
        {
            this.gamesRepository = _gamesRepository;
            this.scoresRepository = _scoresRepository;
        }

        public GameIdDTO AddGame(NewGameDTO newGame)
        {
            if (newGame is null)
            {
                throw new ArgumentNullException(nameof(newGame));
            }

            newGame.IsValid();

            var game = gamesRepository.Add(newGame.ToNewGameModel());

            return new GameIdDTO(game.Id);
        }

        public GameDTO TryGuesst(GuessDTO guess)
        {
            var game = gamesRepository.Get(guess.Id);

            if (game == null)
            {
                throw new GameException("Game Id is not valid");
            }

            if (game.Status != Domian.Enums.GameStatus.pending)
            {
                throw new GameException("Game already ended");
            }

            HandleIfTimesUp(game);
            Check(game, guess.Number);

            gamesRepository.Edit(game);
            var result = new GameDTO();
            result.Status = game.Status;

            if (game.Status == Domian.Enums.GameStatus.won)
            {
                AddToHightScores(game, result);
            }

            return result;
        }

        private void HandleIfTimesUp(Game game)
        {
            if ((DateTime.UtcNow - game.LastAttemptDate).TotalSeconds > 300)
            {
                game.Status = Domian.Enums.GameStatus.lost;
                gamesRepository.Edit(game);
                throw new GameException("Time's up");
            }
        }

        private void Check(Game game, int givenNumber)
        {
            if (game.From > givenNumber || game.To < givenNumber)
            {
                throw new GameException($"Number shoud be betwen {game.From} and {game.To}");
            }

            game.AttemptsLeft--;
            if (game.Number == givenNumber)
            {
                game.Status = Domian.Enums.GameStatus.won;
            }
            else if (game.AttemptsLeft < 1)
            {
                game.Status = Domian.Enums.GameStatus.lost;
            }
        }

        private void AddToHightScores(Game game, GameDTO gameDTO)
        {
            var score = game.CalculateScore();
            if (!score.HasValue)
            {
                return;
            }
            
            var position = scoresRepository.TryAdd(new PlayerScore(game.PlayerName, score.Value));


            gameDTO.Place = position;
            gameDTO.Score = score;
            gameDTO.Number = game.Number;
            return;
        }

        private int CalculateScore(Game game)
        {
            if (game is null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            var attempts = (game.Attempts - game.AttemptsLeft) + 1;

            return (game.To - game.From) * 100 / attempts;
        }

        public HighScoreDTO GetHighScore()
        {
            return new HighScoreDTO(scoresRepository.Get().Select(s => new ScoreDTO(s.PlayerName, s.Score.ToString())).ToArray());
        }
    }
}
