using System;

class Aleatorios
{
    private Random random = new Random();

    // Generar un número aleatorio entre dos valores
    public int GenerarNumero(int min, int max)
    {
        return random.Next(min, max + 1);
    }

    // Generar un arreglo de números aleatorios entre dos valores
    public int[] GenerarArreglo(int cantidad, int min, int max)
    {
        int[] arreglo = new int[cantidad];
        for (int i = 0; i < cantidad; i++)
        {
            arreglo[i] = GenerarNumero(min, max);
        }
        return arreglo;
    }
}

class Program
{
    static void Main()
    {
        Aleatorios aleatorio = new Aleatorios();

        Console.WriteLine("=== EJERCICIO 4 ===");
        Console.Write("Ingrese el número mínimo: ");
        int min = Convert.ToInt32(Console.ReadLine());

        Console.Write("Ingrese el número máximo: ");
        int max = Convert.ToInt32(Console.ReadLine());

        Console.Write("¿Cuántos números aleatorios desea generar? ");
        int cantidad = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("\nGenerando un número aleatorio entre los valores indicados...");
        int numero = aleatorio.GenerarNumero(min, max);
        Console.WriteLine($"Número generado: {numero}");

        Console.WriteLine("\nGenerando arreglo de números aleatorios...");
        int[] arreglo = aleatorio.GenerarArreglo(cantidad, min, max);
        Console.WriteLine("Arreglo generado:");
        Console.WriteLine(string.Join(", ", arreglo));

        Console.WriteLine("\nPresiona una tecla para salir...");
        Console.ReadKey();
    }
}
