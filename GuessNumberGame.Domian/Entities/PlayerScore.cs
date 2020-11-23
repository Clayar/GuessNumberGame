using System;
using System.Collections.Generic;
using System.Text;

namespace GuessNumberGame.Domian.Entities
{
    public class PlayerScore
    {
        public string PlayerName;
        public int Score;

        public PlayerScore(string PlayerName, int Score)
        {
            this.PlayerName = PlayerName;
            this.Score = Score;
        }
    }
}
