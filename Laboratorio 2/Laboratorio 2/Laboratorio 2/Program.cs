using System;

namespace Laboratorio2
{
    class Program
    {
        static void Main(string[] args)
        {

            Client client = new Client();
            //Ejmplo utilizando las variables de instancia de Clase.
            client.FirstName = "Eduardo";
            client.LastName = "Marmolejo";
            client.Age = 24;
            client.Id = 1;

            Console.WriteLine(client.GetFullName());
        }
    }

    public class Client
    {
        //Declaradando variables de instancia en clase.
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ushort Age { get; set; }

        public string GetFullName()
        {
            //Utilizando variable de instancia en metodo de la clase.
            return FirstName + "" + LastName;
        }
    }
}