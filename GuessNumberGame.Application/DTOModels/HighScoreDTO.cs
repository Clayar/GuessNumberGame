using Newtonsoft.Json;

namespace GuessNumberGame.Application.DTOModels
{
    public class HighScoreDTO
    {
        public HighScoreDTO(ScoreDTO[] scores)
        {
            Scores = scores;
        }

        [JsonProperty("scores")]
        public ScoreDTO[] Scores { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
