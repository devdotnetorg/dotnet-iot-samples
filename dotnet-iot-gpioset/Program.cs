using System;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Text.RegularExpressions;

namespace dotnet_iot_gpioset
{
    class Program
    {
        private static GpioController controller;
        private static int exitCode=0;
        static void Main(string[] args)
        {
            //run: dotnet-iot-gpioset 1 36=1
            //-----------------------------------------------            
            try
            {
                int? int_gpiochip=null,int_pin=null;
                PinValue? pin_value=null;

                #if DEBUG
                    Console.WriteLine("Debug version");
                    int_gpiochip=1;
                    int_pin=36;
                    pin_value = PinValue.High;
                #endif
                
                if (args.Length==2)
                {
                    //Read args
                    if (int.TryParse(args[0], out int output)) int_gpiochip = output;
                    Regex r = new Regex(@"\d+=\d+");//36=1
                    if (r.IsMatch(args[1])) //check: 36=1
                    {
                        var i = args[1].Split("=");
                        if (int.TryParse(i[0], out output)) int_pin = output;
                        if (int.TryParse(i[1], out output))
                        {
                            pin_value=(output != 0) ? PinValue.High : PinValue.Low;                             
                        }
                    }  
                }
                if((int_gpiochip is null)||(int_pin is null)||(pin_value is null))
                {
                    Console.WriteLine("Error: invalid parameters. Sample: dotnet-iot-gpioset 1 36=1");
                    Environment.Exit(-1);
                }
                Console.WriteLine($"Args gpiochip={int_gpiochip}, pin={int_pin}, value={pin_value}");
                var drvGpio = new LibGpiodDriver(int_gpiochip.Value);
                controller = new GpioController(PinNumberingScheme.Logical, drvGpio);
                //set value
                if(!controller.IsPinOpen(int_pin.Value))
                {
                    controller.OpenPin(int_pin.Value,PinMode.Output);                    
                    controller.Write(int_pin.Value,pin_value.Value);                    
                    controller.ClosePin(int_pin.Value);
                    Console.WriteLine("OK");
                } else
                {
                    Console.WriteLine("Error: Device or resource busy");
                    exitCode=-2;                    
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                exitCode=-3;                
            }
            finally
            {
                if(controller is not null) controller.Dispose();   
            }
            Environment.Exit(exitCode);
        }
    }
}