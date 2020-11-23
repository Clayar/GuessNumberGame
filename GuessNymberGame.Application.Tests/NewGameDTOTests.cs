using GuessNumberGame.Application.DTOModels;
using GuessNumberGame.Application.Infrastructure;
using GuessNumberGame.Domian.Entities;
using System;
using Xunit;

namespace GuessNumberGame.Application.Tests
{
    public class NewGameDTOTests
    {
        [Fact]
        public void NewGameDTOIsValid()
        {
            var newGame = new NewGameDTO
            {
                PlayerName = "Adam",
                Attempts = 4,
                From = 4,
                To = 8
            };

            Assert.True(newGame.IsValid());

            newGame.PlayerName = "";
            Assert.True(!newGame.IsValid());
        }

        [Theory]
        [InlineData(4, 0, 2, "From not valid")]
        [InlineData(4, 1, 2, "To not valid")]
        [InlineData(4, 5, 6, "Too small range")]
        [InlineData(0, 5, 11, "Attempts not valid")]
        public void NewGameDTOIsValidFails(int attempts, int from, int to, string exceptionMessage)
        {
            var newGame = new NewGameDTO
            {
                PlayerName = "Adam",
                Attempts = attempts,
                From = from,
                To = to
            };

            var ex = Assert.Throws<GameException>(() => newGame.IsValid());
            Assert.Equal(exceptionMessage, ex.Message);
        }

        [Fact]
        public void NewGameDTOToNewGameModelPass()
        {
            var newGame = new NewGameDTO
            {
                PlayerName = "Adam",
                Attempts = 3,
                From = 4,
                To = 8
            }.ToNewGameModel();

            var newGameExpected = new Game
            {
                PlayerName="Adam",
                Attempts = 3,
                AttemptsLeft = 3,
                From = 4,
                To = 8,
                Status = Domian.Enums.GameStatus.pending,
                LastAttemptDate = newGame.LastAttemptDate,
                Number = newGame.Number
            };

            Assert.Equal(newGame, newGameExpected);
        }
    }
}
