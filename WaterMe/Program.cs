using System;
using Iot.Device.CpuTemperature;
using System.Threading;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Iot.Device.DHTxx;
using Newtonsoft.Json.Serialization;

namespace WaterMe
{
    class Program
    {
        // Async method to send data to the api
        private static async Task PostTemperature(Metric metric, string url)
        {
            // Serialize the message for sending it over http
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var json = JsonConvert.SerializeObject(metric, settings);

            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var msg = await client.PostAsync(url, new StringContent(json, Encoding.UTF8));
                Console.Write(msg);
            }
            catch (System.Exception ex)
            {
                Console.Write($"Failed to post message: {ex.Message}");
            }
        }

        // Client used to send data to the api. This object is capable of sending  and retreiving data from the web.
        private static readonly HttpClient client = new HttpClient();

        // Used to access the cpu temperature on RPi
        static CpuTemperature rpiTemp = new CpuTemperature();
        static async Task Main(string[] args)
        {
            // load the config like connection strings.
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string url = configuration["ConnectionStringSetting"];

            string hostName = System.Net.Dns.GetHostName();

            // GPIO Pin
            using (Dht22 dht = new Dht22(4))
            {
                while (true)
                {
                    //Temperature temperature = dht.Temperature;
                    double humidity = dht.Humidity;
                    double temp = dht.Temperature.Celsius;

                    if (humidity > 0 && humidity < 100)
                    {
                        Metric metric = new Metric
                        {
                            HostName = hostName,
                            TempCelsius = temp,
                            Humidity = humidity
                        };
                        Console.WriteLine($"T: {metric.TempCelsius} H: {metric.Humidity}");

                        // Call the method to send data to the api as task. Task will run until completed.
                        try
                        {
                            await PostTemperature(metric, url);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Unable to read sensor (t: {dht.Temperature.Celsius} h: {dht.Humidity}) ");
                    }
                    Console.WriteLine("Sleeping for 10 seconds...");
                    Thread.Sleep(10000); // sleep for 10 seconds
                }
            }
        }
    }
}