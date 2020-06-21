using System;
using System.Text.Json.Serialization;

namespace WaterMeCollectorFunction
{
    public class Metric
    {
        [JsonPropertyName("Host")]
        public string Host { get; set; }

        [JsonPropertyName("TempCelsius")]
        public double TempCelsius { get; set; }
        public DateTime DateTime { get; set; }
    }
}