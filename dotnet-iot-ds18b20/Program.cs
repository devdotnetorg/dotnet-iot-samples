using System;
using System.Linq;
using Iot.Device.OneWire;

namespace dotnet_iot_ds18b20
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DS18B20!");
            try
            {
                var devOneWire = OneWireThermometerDevice.EnumerateDevices().FirstOrDefault();
                double temp=devOneWire.ReadTemperatureAsync().Result.DegreesCelsius;
                Console.WriteLine($"Temperature = {Math.Round(temp, 2, MidpointRounding.AwayFromZero)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No 1-Wire sensors found. Exception: {ex.Message}");                
            }            
        }
    }
}
