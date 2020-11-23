using System;

namespace GuessNumberGame.Application.Infrastructure
{
    public class GameException : Exception
    {
        public GameException(string message) : base(message)
        {
        }
    }
}