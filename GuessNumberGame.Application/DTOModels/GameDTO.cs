using GuessNumberGame.Domian.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GuessNumberGame.Application.DTOModels
{
    public class GameDTO
    {
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public GameStatus Status { get; set; }

        [JsonProperty("number", NullValueHandling = NullValueHandling.Ignore)]
        public int? Number { get; set; }

        [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
        public int? Score { get; set; }

        [JsonProperty("place", NullValueHandling = NullValueHandling.Ignore)]
        public int? Place { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
