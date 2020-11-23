using GuessNumberGame.Domian.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GuessNumberGame.DataRepository.Repositories
{
    public class ScoresRepository : IScoresRepository
    {
        private string filePath { get
            {
                return Directory.GetCurrentDirectory() + "\\WriteText.json";
            }
        }

        public ScoresRepository()
        {
        }

        public int? TryAdd(PlayerScore score)
        {
            if (score == null)
            {
                throw new ArgumentNullException("Score");
            }

            var highScores = this.Get();

            highScores.Add(score);

            highScores = highScores.OrderByDescending(s => s.Score).Take(30).ToList();

            var highScoresArray = highScores.ToArray();
            this.Save(highScores);
            var position = highScores.IndexOf(score) + 1;

            if (position > 0)
            {
                return position;
            }

            return null;
        }

        public IList<PlayerScore> Get()
        {
            var highScores = new List<PlayerScore>();
            if (File.Exists(filePath))
            {
                string[] lines = System.IO.File.ReadAllLines(filePath);
                var json = string.Join("", lines);
                try
                {
                    var scoresArray = JsonConvert.DeserializeObject<PlayerScore[]>(json);
                    highScores.AddRange(scoresArray);
                }
                catch
                {
                    Console.WriteLine("Can't deserialize highscore file ");
                }
            }
            return highScores;
        }

        private void Save(IList<PlayerScore> scores)
        {
            var json = JsonConvert.SerializeObject(scores.ToArray());
            System.IO.File.WriteAllText(filePath, json);
            return;
        }
    }
}
