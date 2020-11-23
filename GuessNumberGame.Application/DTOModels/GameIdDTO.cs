using Newtonsoft.Json;

namespace GuessNumberGame.Application.DTOModels
{
    public class GameIdDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public GameIdDTO(string id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
