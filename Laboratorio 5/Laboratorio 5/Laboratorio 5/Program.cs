class Program
{
    private int[] sueldos; //declaramos un vector 

    public void Cargar()
    {
        sueldos = new int[6]; // Inicializamos el vector en 5
        for (int f = 1; f <= 5; f++)
        {
            Console.Write("Ingree sueldo del operario " + f + ":");
            String linea;
            linea = Console.ReadLine();
            sueldos[f] = int.Parse(linea);// Asignamos los 5 sueldos al vector

        }
    }

    //Muestra los sueldos de los operarios en el vector sueldos [f]
    public void Imprimir()
    {
        Console.Write("Los sueldos ingresados son: \n");
        for (int f = 1; f <= 5; f++)
        {
            Console.Write("[" + sueldos[f] + "]");
        }
        Console.ReadKey();
    }

    //main Principal
    static void Main(string[] args)
    {
        PruebaVector1 pv = new PruebaVector1();
        pv.cargar();
        pv.imprimir();
    }
}