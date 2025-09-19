class program
{
    static void main(string[] args)
    {
        Console.WriteLine("digite el radio del circulo");
        double radio = double.Parse (Console.ReadLine());
        double area = Math.Pow(radio,2) * Math.PI;
        Console.WriteLine($"el area del circulo es: {area}");
    }
}