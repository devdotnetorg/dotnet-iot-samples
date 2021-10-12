using System;
using System.Runtime.InteropServices;

namespace СonsoleApp50
{    
    class Program
    {        
        static void Main(string[] args)
        {
            Console.WriteLine("Test .NET console application!");
            var str_Framework=RuntimeInformation.FrameworkDescription;
            var str_OSArch=RuntimeInformation.OSArchitecture.ToString();            
            var str_OSDesc= RuntimeInformation.OSDescription;  
            var str_OSIdent=RuntimeInformation.RuntimeIdentifier;          
            //output
            Console.WriteLine($"Версия .NET: {str_Framework}");
            Console.WriteLine($"Архитектура ОС: {str_OSArch}");
            Console.WriteLine($"Версия ОС: {str_OSDesc}");            
            Console.WriteLine($"Идентификатор ОС: {str_OSIdent}");
            //error
            Console.WriteLine("Генерировать исключение [yes/No]? ");            
            var input_key=Console.ReadKey().Key;
            Console.Write("\n");
            if (input_key==ConsoleKey.Y)
            {   
                    Console.Write("Генерация исключения");
                        throw new Exception("А вот и ошибочка!");
            }
             Console.WriteLine("Завершение работы программы");            
        }
    }
}