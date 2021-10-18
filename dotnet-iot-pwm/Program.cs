using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Device.Pwm;

namespace dotnet_iot_pwm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting PWM Controller - Simple Demo");
            const int chip= 0; // sysfs: /sys/class/pwm/pwmchip0 
            const int channel = 0; // sysfs: /sys/class/pwm/pwmchip0/pwm0
            //
            const int frequency = 200; //Hz
            using (var _pwm = PwmChannel.Create(chip, channel, frequency,dutyCyclePercentage:0))
            {
                _pwm.Start();
                for (double fill = 0; fill <= 1;fill+=0.2)
                    {
                        Console.WriteLine("Duty cycle: {0, 0:n0}%", fill * 100);
                        _pwm.DutyCycle = fill;
                        Thread.Sleep(1700);
                    }
                _pwm.Stop();
            }
            Console.WriteLine("Closing the program");     
        }
    }
}
