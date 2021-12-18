using System;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Diagnostics;
using System.Threading;
using Iot.Device.Button;

namespace dotnet_iot_button_from_nanoframework
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Driver Button: Iot.Device.Button
            sample: https://github.com/nanoframework/nanoFramework.IoT.Device/blob/develop/devices/Button/README.md
            */

            //for Linux
            const int GPIOCHIP = 1;
            const int BUTTON_PIN = 38;
            //const int BUTTON_PIN = 68; //Pull-up            

            GpioController controller;
            var drvGpio = new LibGpiodDriver(GPIOCHIP);
            controller = new GpioController(PinNumberingScheme.Logical, drvGpio);

            // Initialize a new button with the corresponding button pin
            var timespanDoublePress = TimeSpan.FromMilliseconds(300);
            var timespanHolding  = TimeSpan.FromMilliseconds(100);
            var button = new GpioButton(buttonPin: BUTTON_PIN, doublePress: timespanDoublePress, 
                holding: timespanHolding, gpio: controller,false,PinMode.Input);
            
            Debug.WriteLine("Button is initialized, starting to read state");

            // Enable or disable holding or doublepress events
            button.IsDoublePressEnabled = true;
            button.IsHoldingEnabled = true;
            
            // Write to debug if the button is down
            button.ButtonDown += (sender, e) =>
            {                
                Debug.WriteLine($"buttondown IsPressed={button.IsPressed}");
            };

            // Write to debug if the button is up
            button.ButtonUp += (sender, e) =>
            {
                Debug.WriteLine($"buttonup IsPressed={button.IsPressed}");
            };

            // Write to debug if the button is pressed
            button.Press += (sender, e) =>
            {
                Debug.WriteLine($"Press");
            };

            // Write to debug if the button is double pressed
            button.DoublePress += (sender, e) =>
            {
                Debug.WriteLine($"Double press");
            };

            // Write to debug if the button is held and released
            button.Holding += (sender, e) =>
            {
                switch (e.HoldingState)
                {
                    case ButtonHoldingState.Started:
                        Debug.WriteLine($"Holding Started");
                        break;
                    case ButtonHoldingState.Completed:
                        Debug.WriteLine($"Holding Completed");
                        break;
                }
            };

            //Thread.Sleep(Timeout.Infinite);
            //Console            
            string cki;
            while (true)
            {
                Console.Write("Press any key, or 'X' to quit, or ");
                Console.WriteLine("CTRL+C to interrupt the read operation:");
                // Start a console read operation. Do not display the input.                
                cki = Console.In.ReadLineAsync().GetAwaiter().GetResult();
                // Announce the name of the key that was pressed .
                Console.WriteLine($"  Key pressed: {cki}\n");
                // Exit if the user pressed the 'X' key.               
                if (cki == "X"||cki == "x") break;
            }
            //Dispose
            button.Dispose();
        }
    }
}
