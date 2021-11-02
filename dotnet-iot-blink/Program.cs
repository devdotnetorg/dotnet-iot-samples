using System;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Threading;

namespace dotnet_iot_blink
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Blink an LED
            *
            https://docs.microsoft.com/en-us/dotnet/iot/tutorials/blink-led
            *
            */
            Console.WriteLine("Blinking LED.");
            //for Linux
            const int GPIOCHIP = 1;
            const int LED_PIN = 143; //GPIO143 - bananapi-m64:blue. Board: Banana Pi BPI-M64
            GpioController controller;
            var drvGpio = new LibGpiodDriver(GPIOCHIP);
            controller = new GpioController(PinNumberingScheme.Logical, drvGpio);
            //
            controller.OpenPin(LED_PIN, PinMode.Output);
            bool ledOn = true;
            while (true)
            {
                controller.Write(LED_PIN, ((ledOn) ? PinValue.High : PinValue.Low));
                Thread.Sleep(1000);
                ledOn = !ledOn;
            }
        }
    }
}
