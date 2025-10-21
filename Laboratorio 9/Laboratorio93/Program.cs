using System;

class Program
{
    static void Main()
    {
        Console.Write("Ingrese el lado 1: ");
        double a = Convert.ToDouble(Console.ReadLine());
        Console.Write("Ingrese el lado 2: ");
        double b = Convert.ToDouble(Console.ReadLine());
        Console.Write("Ingrese el lado 3: ");
        double c = Convert.ToDouble(Console.ReadLine());

        if (a + b > c && a + c > b && b + c > a)
        {
            if (a == b && b == c)
                Console.WriteLine("El triángulo es equilátero.");
            else if (a == b || a == c || b == c)
                Console.WriteLine("El triángulo es isósceles.");
            else
                Console.WriteLine("El triángulo es escaleno.");
        }
        else
        {
            Console.WriteLine("Los lados no forman un triángulo válido.");
        }
    }
}
