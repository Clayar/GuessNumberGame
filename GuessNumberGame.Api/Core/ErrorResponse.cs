using Newtonsoft.Json;

namespace GuessNumberGame.Api.Core
{
    public class ErrorResponse
    {
        [JsonProperty("error")]
        public string Error
        {
            get;
            set;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}