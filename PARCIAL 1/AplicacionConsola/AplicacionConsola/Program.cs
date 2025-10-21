using System;

class Program
{
    static void Main()
    {
        int N;

        
        do
        {
            Console.Write("Ingrese el valor de N (debe ser impar): ");
        } while (!int.TryParse(Console.ReadLine(), out N) || N < 3 || N % 2 == 0);

        int[,] matriz = new int[N, N];
        int centro = N / 2;
        long suma = 0;
        Random rnd = new Random();
        for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
                matriz[i, j] = (i == 0 || i == N - 1 || j == centro) 
                    ? rnd.Next(101, 201)
                    : 0;

       
        Console.WriteLine($"\nMatriz {N}x{N}:\n");
        //Metodo para mostrar la matriz y la suma
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                Console.Write($"{matriz[i, j],4}");
                suma += matriz[i, j];
            }
            Console.WriteLine();
        }

        Console.WriteLine($"\nSuma total de los valores aleatorios: {suma}");
    }
}
