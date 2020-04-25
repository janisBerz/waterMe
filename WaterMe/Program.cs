using System;
using Iot.Device.CpuTemperature;
using System.Threading;

namespace IOT
{
    class Program
    {
        static CpuTemperature temperature = new CpuTemperature();
        static void Main(string[] args)
        {
            while (true)
            {
                if (temperature.IsAvailable)
                {
                    Console.WriteLine($"The CPU temperature in Celsius is { temperature.Temperature.Celsius}" +"C");
                    Console.WriteLine($"The CPU temperature in Fahrenheit is { temperature.Temperature.Fahrenheit} " + "F");
                }

                Thread.Sleep(2000); // sleep for 2000 milliseconds, 2 seconds
            }
        }
    }
}