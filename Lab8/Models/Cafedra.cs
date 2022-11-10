using Newtonsoft.Json;

namespace Lab8.Models
{
    public class Cafedra
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("fio")]
        public string Fio { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("facult")]
        public string Facult { get; set; }
    }
}
