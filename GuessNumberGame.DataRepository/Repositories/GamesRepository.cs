using GuessNumberGame.Domian.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GuessNumberGame.DataRepository.Repositories
{
    public class GamesRepository : IGamesRepository
    {
        private List<Game> games = new List<Game>();

        public GamesRepository()
        {
        }

        public Game Add(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException("Game");
            }

            var counter = 0;
            string id = this.GetRandomId();

            while (games.Any(g => g.Id == id) || counter > 10)
            {
                counter++;
                id = this.GetRandomId();
            }

            if (games.Any(g => g.Id == id))
            {
                throw new Exception("Can't get random Id for the game");
            }

            game.Id = id;
            games.Add(game);
            return game;
        }

        public Game Get(string id)
        {
            return games.FirstOrDefault(g => g.Id == id);
        }

        public Game Edit(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException("Game");
            }

            var exsistingGame = this.Get(game.Id);

            if (exsistingGame == null)
            {
                throw new Exception("Trying to modify non existing game");
            }

            exsistingGame = game;
            exsistingGame.LastAttemptDate = DateTime.UtcNow;

            return exsistingGame;

        }

        private string GetRandomId()
        {
            int length = 5;

            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            return str_build.ToString();
        }
    }
}
