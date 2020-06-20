using Iot.Device.DHTxx;
using Iot.Units;
using System;
using System.Threading;

namespace IOT
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DHTSensor inside = new DHTSensor(16, DhtType.Dht22))
            {
                double relativeHumidity;

                while (true)
                {
                    if (!inside.TryGetTemperatureAndHumidity(out Temperature temperature, out relativeHumidity) || inside.Temperature.Celsius > 60d || inside.Humidity > 100d)
                    {
                        Console.WriteLine(" failed obtaining inside readings");
                        continue;
                    }
                    Console.WriteLine($"C: {temperature.Celsius.ToString()} H: {relativeHumidity}");
                    Thread.Sleep(1000);
                }
            }

            // using (Dht22 inside = new Dht22(16))
            // {
            //     while (true)
            //     {
            //         System.Console.WriteLine("pin 16");
            //         System.Console.WriteLine($"C: {inside.Temperature.Celsius} H: {inside.Humidity}");
            //         Thread.Sleep(1000);
            //     }

            // }

            // using (Dht22 inside4 = new Dht22(7))
            // {
            //     for (int i = 0; i < 10; i++)
            //     {
            //         System.Console.WriteLine("pin 4");
            //         System.Console.WriteLine($"C: {inside4.Temperature.Celsius} H: {inside4.Humidity}");
            //         Thread.Sleep(1000);
            //     }
            // }
        }
    }
}
