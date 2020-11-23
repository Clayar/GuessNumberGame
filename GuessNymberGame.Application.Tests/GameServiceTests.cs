using GuessNumberGame.Application.DTOModels;
using GuessNumberGame.Application.Infrastructure;
using GuessNumberGame.Application.Services;
using GuessNumberGame.DataRepository.Repositories;
using GuessNumberGame.Domian.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace GuessNumberGame.Application.Tests
{
    public class GameServiceTests
    {

        private IGameService GetSampleGameService(Game game = null)
        {
            var scores = new List<PlayerScore>
            {
                new PlayerScore("ABC", 1000),
                new PlayerScore("CBA", 100)
            };
            if(game == null)
            {
                game = new NewGameDTO().ToNewGameModel();
                game.Id = "ABCDE";
            }

            var scoresRepo = new Mock<IScoresRepository>();
            scoresRepo.Setup(m => m.Get()).Returns(scores);
            scoresRepo.Setup(m => m.TryAdd(It.IsAny<PlayerScore>())).Returns(3);

            var gamesRepo = new Mock<IGamesRepository>();
            gamesRepo.Setup(m => m.Get(It.IsAny<string>())).Returns(game);
            gamesRepo.Setup(m => m.Add(It.IsAny<Game>())).Returns<Game>(m => 
            { m.Id = "ABCDE";
                return m; });
            gamesRepo.Setup(m => m.Edit(It.IsAny<Game>())).Returns<Game>(g=>g);

            return new GameService(gamesRepo.Object, scoresRepo.Object);
        }
        [Fact]
        public void GetScores()
        {
            var gameService = GetSampleGameService(null);
            
            var result = gameService.GetHighScore();

            Assert.Equal(2, result.Scores.Length);
            Assert.True(result.Scores.First(i => i.PlayerName == "ABC" && i.Score == 1000.ToString()) != null);
        }

        [Fact]
        public void AddGame_Succes()
        {
            var gameService = GetSampleGameService();

            var newGame = new NewGameDTO();

            var game = gameService.AddGame(newGame);

            Assert.True(!string.IsNullOrEmpty(game.Id));


        }

        [Fact]
        public void TryGuessNumber_Miss()
        {
            var game = new NewGameDTO().ToNewGameModel();
            game.Id = "ABCDE";

            var gameService = GetSampleGameService(game);

            for (int i = 1; i < game.Attempts; i++)
            {
                var loopResult = gameService.TryGuesst(new GuessDTO
                {
                    Id = game.Id,
                    Number = game.Number - 1
                });

                Assert.Equal(Domian.Enums.GameStatus.pending, loopResult.Status);
                Assert.Equal(game.Attempts - i, game.AttemptsLeft);
            }
            var result = gameService.TryGuesst(new GuessDTO
            {
                Id = game.Id,
                Number = game.Number - 1 < game.From ? game.Number + 1 : game.Number - 1
            });

            Assert.Equal(Domian.Enums.GameStatus.lost, result.Status);
            Assert.Equal(0, game.AttemptsLeft);

        }

        [Fact]
        public void TryGuessNumber_NumberOutOfRange()
        {
            var game = new NewGameDTO().ToNewGameModel();
            game.Id = "ABCDE";

            var gameService = GetSampleGameService(game);

            var guess = new GuessDTO
            {
                Id = game.Id,
                Number = game.From - 1
            };

            Assert.Throws<GameException>(() => gameService.TryGuesst(guess));
        }

        [Fact]
        public void TryGuessNumber_HitOnFirstTry()
        {
            var game = new NewGameDTO().ToNewGameModel();
            game.Id = "ABCDE";

            var gameService = GetSampleGameService(game);

            var result = gameService.TryGuesst(new GuessDTO
            {
                Id = game.Id,
                Number = game.Number
            });

            Assert.Equal(Domian.Enums.GameStatus.won, result.Status);
            Assert.Equal(game.Attempts - 1, game.AttemptsLeft);
            Assert.Equal(game.Number, result.Number);
            Assert.Equal(3, result.Place);
        }

        [Fact]
        public void TryGuessNumber_HitOnLastTry()
        {
            var game = new NewGameDTO().ToNewGameModel();
            game.Id = "ABCDE";

            var gameService = GetSampleGameService(game);

            for (int i = 1; i < game.Attempts; i++)
            {
                var loopResult = gameService.TryGuesst(new GuessDTO
                {
                    Id = game.Id,
                    Number = game.Number - 1 < game.From ? game.Number + 1 : game.Number - 1
                });

                Assert.Equal(Domian.Enums.GameStatus.pending, loopResult.Status);
                Assert.Equal(game.Attempts - i, game.AttemptsLeft);
            }
            var result = gameService.TryGuesst(new GuessDTO
            {
                Id = game.Id,
                Number = game.Number
            });

            Assert.Equal(Domian.Enums.GameStatus.won, result.Status);
            Assert.Equal(0, game.AttemptsLeft);
        }

        [Fact]
        public void TryGuessNumber_TimeUp()
        {
            var game = new NewGameDTO().ToNewGameModel();
            game.Id = "ABCDE";
            game.LastAttemptDate = game.LastAttemptDate.AddDays(-1);

            var gameService = GetSampleGameService(game);


            var guess = new GuessDTO
            {
                Id = game.Id,
                Number = game.Number
            };

            Assert.Throws<GameException>(() => gameService.TryGuesst(guess));
        }
    }
}
