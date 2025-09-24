class Program
{
    static void Main (string[] args)
    {
        try
        {
            int[] myNumbers = {1, 2, 3};
            Console.WriteLine(myNumbers[10]);
        }
        catch (Exception e)
        {
            Console.WriteLine("Algo salio mal, valida el indice del arreglo");
        }
        finally
        {
            Console.WriteLine("Continuación de la aplicacion, luego del bloque try/catch");
        }
    }
}