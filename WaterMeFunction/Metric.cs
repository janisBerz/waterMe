using System;
using System.Text.Json.Serialization;

namespace WaterMeCollectorFunction
{
    public class Metric
    {
        [JsonPropertyName("hostName")]
        public string HostName { get; set; }

        [JsonPropertyName("tempCelsius")]
        public double TempCelsius { get; set; }

        [JsonPropertyName("humidity")]
        public double Humidity { get; set; }
        public DateTime DateTime { get; set; }
    }
}