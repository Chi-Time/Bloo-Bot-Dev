using Newtonsoft.Json;

namespace BlooBot.Objects
{
    class BotConfig
    {
        [JsonProperty ("Token")]
        public string Token { get; set; }

        [JsonProperty ("Prefix")]
        public string CommandPrefix { get; set; }
    }
}
