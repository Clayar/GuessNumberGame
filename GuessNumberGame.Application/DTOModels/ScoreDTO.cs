using Newtonsoft.Json;

namespace GuessNumberGame.Application.DTOModels
{
    public class ScoreDTO
    {
        public ScoreDTO(string playerName, string score)
        {
            PlayerName = playerName;
            Score = score;
        }

        [JsonProperty("playerName")]
        public string PlayerName { get; set; }

        [JsonProperty("score")]
        public string Score { get; set; }
    }
}
