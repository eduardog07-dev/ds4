using System;

namespace Laboratorio21
{
    class Program
    {
        public static void Main()
        {
            //Asignando valor a variable estatica.
            MyClass.valor = 1;
            Console.WriteLine(MyClass.valor);
        }
    }
    public class MyClass
    {
        //Declarando variable estatica.
        public static int valor;
    }
}