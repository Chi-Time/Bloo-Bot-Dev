using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Bloo_Bot
{
    class BotConfig
    {
        [JsonProperty ("Token")]
        public string Token { get; set; }

        [JsonProperty ("Prefix")]
        public string CommandPrefix { get; set; }
    }
}
