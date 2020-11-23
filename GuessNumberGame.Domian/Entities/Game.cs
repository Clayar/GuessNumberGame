using GuessNumberGame.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GuessNumberGame.Domian.Entities
{
    public class Game : IEquatable<Game>
    {
        public string Id;
        public string PlayerName;
        public int From;
        public int To;
        public int Number;
        public int Attempts;
        public int AttemptsLeft;
        public GameStatus Status;
        public DateTime LastAttemptDate;

        public int? CalculateScore()
        {
            if(this.Status != GameStatus.won)
            {
                return 0;
            }
            var attempts = (this.Attempts - this.AttemptsLeft) + 1;

            return (this.To - this.From) * 100 / attempts;
        }

        public bool Equals([AllowNull] Game other)
        {
            if(other == null)
            {
                return false;
            }

            if (this.Id != other.Id
                || this.PlayerName != other.PlayerName
                || this.To != other.To
                || this.From != other.From
                || this.LastAttemptDate != other.LastAttemptDate
                || this.Attempts != other.Attempts
                || this.AttemptsLeft != other.AttemptsLeft
                || this.Status != other.Status
                || this.Number != other.Number
                )
            {
                return false;
            }
            return true;
        }
    }
}
