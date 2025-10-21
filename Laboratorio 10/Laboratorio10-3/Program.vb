Module peso
    Sub Main()
        'Declaracion de variables
        Dim M As Double
        Dim G As Double
        Dim P As Double

        'Ingresar valores para las variables
        G = 9.8

        Console.Write("Ingrese la masa del objeto:")

        ' CORRECCIÓN: Se usa Console.ReadLine() para leer y Double.Parse para convertir a Double
        M = Double.Parse(Console.ReadLine())

        'Realizar los procesos
        P = M * G

        'Mostrar resultados
        Console.WriteLine("Peso del objeto: {0}", P)
        Console.ReadKey()
    End Sub
End Module