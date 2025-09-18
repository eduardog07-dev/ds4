using System;

namespace Laboratorio32
{
    class CalculosMatematicos
    {
        public static double CalculoArea(double radio)
        {
            return Math.PI * Math.Pow(radio, 2);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese el radio del círculo:");
            double radio = double.Parse(Console.ReadLine());

            double area = CalculosMatematicos.CalculoArea(radio);

            Console.WriteLine($"El área del círculo con radio {radio} es: {area:F2}");
        }
    }
}
