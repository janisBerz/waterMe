using System;
using System.Text.Json.Serialization;

namespace WaterMeCollectorFunction
{
    public class Metric
    {
        [JsonPropertyName("HostName")]
        public string HostName { get; set; }

        [JsonPropertyName("TempCelsius")]
        public double TempCelsius { get; set; }

        [JsonPropertyName("Humidity")]
        public double Humidity { get; set; }
        public DateTime DateTime { get; set; }
    }
}