using Newtonsoft.Json;

namespace GuessNumberGame.Application.DTOModels
{
    public class GuessDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }
    }
}
