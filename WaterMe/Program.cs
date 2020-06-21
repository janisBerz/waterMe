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

namespace WaterMe
{
    class Program
    {
        // Async method to send data to the api
        private static async Task PostTemperature(Temperature temp)
        {
            string url = "http://192.168.88.108:7071/api/waterMeFunc";

            var jsonSerialoized = JsonConvert.SerializeObject(temp);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var msg = await client.PostAsync(url, new StringContent(jsonSerialoized, Encoding.UTF8));

            Console.Write(msg);
        }

        // Client used to send data to the api. This object is capable of sending  and retreiving data from the web.
        private static readonly HttpClient client = new HttpClient();

        // Used to access the cpu temperature on RPi
        static CpuTemperature rpiTemp = new CpuTemperature();
        static async Task Main(string[] args)
        {
              var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            if (rpiTemp.IsAvailable)
            {
                Console.WriteLine($"The CPU temperature in Celsius is {rpiTemp.Temperature.Celsius}" + "C");
                Temperature temperature = new Temperature();
                temperature.Temp = rpiTemp.Temperature.Celsius;
                // Call the method to send data to the api as task. Task will run until completed.

                try
                {
                    await PostTemperature(temperature);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.StackTrace);
                }
            }
            Thread.Sleep(2000); // sleep for 2000 milliseconds, 2 seconds

            while (rpiTemp.IsAvailable)
            {
                if (rpiTemp.IsAvailable)
                {
                    Console.WriteLine($"The CPU temperature in Celsius is {rpiTemp.Temperature.Celsius}" + "C");
                    Temperature temperature = new Temperature();
                    temperature.Temp = rpiTemp.Temperature.Celsius;
                    // Call the method to send data to the api as task. Task will run until completed.

                    try
                    {
                        await PostTemperature(temperature);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.StackTrace);
                    }
                }
                Thread.Sleep(2000); // sleep for 2000 milliseconds, 2 seconds
            }
        }
    }
}