using System;

class Program
{
    static void Main()
    {
        Console.Write("Ingrese el precio del producto: ");
        double precio = Convert.ToDouble(Console.ReadLine());

        while (precio <= 0)
        {
            Console.Write("El precio debe ser positivo. Ingrese nuevamente: ");
            precio = Convert.ToDouble(Console.ReadLine());
        }

        Console.Write("Forma de pago (efectivo o tarjeta): ");
        string formaPago = Console.ReadLine().ToLower();

        if (formaPago == "tarjeta")
        {
            string cuenta;
            do
            {
                Console.Write("Ingrese el número de cuenta (16 dígitos): ");
                cuenta = Console.ReadLine();
            } while (cuenta.Length != 16 || !long.TryParse(cuenta, out _));

            Console.WriteLine($"Pago realizado con tarjeta. Cuenta: {cuenta}");
        }
        else
        {
            Console.WriteLine("Pago realizado en efectivo.");
        }
    }
}
