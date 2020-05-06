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
            DateTime dateTime = DateTime.UtcNow.Date;

            while (true)
            {
                if (temperature.IsAvailable)
                {
                    double tempr = temperature.Temperature.Celsius;
                    tempr = Math.Round(tempr, 2);
                    Console.WriteLine($"The CPU temperature in Celsius is {tempr} C {dateTime.ToString()}");
                }

                Thread.Sleep(2000); // sleep for 2000 milliseconds, 2 seconds
            }
        }
    }
}