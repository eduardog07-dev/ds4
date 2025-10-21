using System;
using System.Collections.Generic;

class Aleatorios
{
    private Random random = new Random();

    // Generar un número entre dos valores
    public int GenerarNumero(int min, int max)
    {
        return random.Next(min, max + 1);
    }

    // Generar un arreglo de números no repetidos entre dos valores
    public int[] GenerarArregloNoRepetido(int cantidad, int min, int max)
    {
        if (cantidad > (max - min + 1))
            throw new ArgumentException("El rango no permite generar esa cantidad de números sin repetirse.");

        HashSet<int> numeros = new HashSet<int>();
        while (numeros.Count < cantidad)
        {
            numeros.Add(GenerarNumero(min, max));
        }

        int[] resultado = new int[numeros.Count];
        numeros.CopyTo(resultado);
        return resultado;
    }
}

class Program
{
    static void Main()
    {
        Aleatorios aleatorio = new Aleatorios();
        Console.Write("Ingrese el número mínimo: ");
        int min = Convert.ToInt32(Console.ReadLine());

        Console.Write("Ingrese el número máximo: ");
        int max = Convert.ToInt32(Console.ReadLine());

        Console.Write("¿Cuántos números aleatorios desea generar? ");
        int cantidad = Convert.ToInt32(Console.ReadLine());

        try
        {
            int[] arregloNoRepetido = aleatorio.GenerarArregloNoRepetido(cantidad, min, max);

            Console.WriteLine("\nNúmeros generados (sin repetirse):");
            Console.WriteLine(string.Join(", ", arregloNoRepetido));
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("\nError: " + e.Message);
        }

        Console.WriteLine("\nPresiona una tecla para salir...");
        Console.ReadKey();
    }
}
