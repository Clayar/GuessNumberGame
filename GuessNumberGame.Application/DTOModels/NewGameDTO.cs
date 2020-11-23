using GuessNumberGame.Application.Infrastructure;
using GuessNumberGame.Domian.Entities;
using Newtonsoft.Json;
using System;

namespace GuessNumberGame.Application.DTOModels
{
    public class NewGameDTO
    {
        [JsonProperty("playerName")]
        public string PlayerName { get; set; } = "'Unnamed player";
        [JsonProperty("from")]
        public int From { get; set; } = 1;
        [JsonProperty("to")]
        public int To { get; set; } = 9;
        [JsonProperty("attempts")]
        public int Attempts { get; set; } = 3;

        public Game ToNewGameModel()
        {
            return new Game
            {
                PlayerName = this.PlayerName,
                Attempts = this.Attempts,
                AttemptsLeft = this.Attempts,
                LastAttemptDate = DateTime.UtcNow,
                Status = Domian.Enums.GameStatus.pending,
                Number = new Random().Next(From, To+1),
                From = this.From,
                To = this.To
                
            };
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(this.PlayerName))
            {
                return false;
            }
            
            if(From <= 0)
            {
                throw new GameException(nameof(From) + " not valid");
            }
            
            if(To <= 2)
            {
                throw new GameException(nameof(To) + " not valid");
            }
            
            if (To - From < 2)
            {
                throw new GameException("Too small range");
            }

            if(Attempts < 1)
            {
                throw new GameException(nameof(Attempts) + " not valid");
            }

            return true;
        }
    }
}
