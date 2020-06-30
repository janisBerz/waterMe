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
using System.Device.Gpio;
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

        static void Main(string[] args)
        {
            var pin = 5;
            var lightTimeInMilliseconds = 1000;
            var dimTimeInMilliseconds = 200;

            Console.WriteLine($"Let's blink an LED!");
            using (GpioController controller = new GpioController())
            {
                controller.OpenPin(pin, PinMode.Output);
                Console.WriteLine($"GPIO pin enabled for use: {pin}");

                Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
                {
                    controller.Dispose();
                };

                while (true)
                {
                    Console.WriteLine($"Light for {lightTimeInMilliseconds}ms");
                    controller.Write(pin, PinValue.High);
                    Thread.Sleep(lightTimeInMilliseconds);
                    Console.WriteLine($"Dim for {dimTimeInMilliseconds}ms");
                    controller.Write(pin, PinValue.Low);
                    Thread.Sleep(dimTimeInMilliseconds);
                }
            }
        }
    }
}