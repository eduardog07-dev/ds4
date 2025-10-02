Public Class Perro
    Public nombre As String
    Public raza As String
    ' CORRECCIÓN: altura debe ser un tipo numérico (Single o Double) si se va a usar en cálculos.
    Public altura As Single

    Public Function comer(carne As String) As String
        ' Devuelve una cadena descriptiva.
        Return nombre & " mide " & altura & " y comerá " & carne
    End Function

    Public Sub dormir()
        ' Implementación del método dormir
    End Sub

    Public Sub ladrar()
        ' Implementación del método ladrar
    End Sub

    Public Function calcularCosto(costo As Double, impuesto As Double) As Double
        Dim preciototal As Double
        preciototal = costo + (costo * impuesto)
        Return preciototal
    End Function

    Public Sub New()
        ' Constructor sin parámetros
    End Sub

    Public Sub New(nombre As String, raza As String, altura As Single)
        Me.nombre = nombre
        Me.raza = raza
        Me.altura = altura
    End Sub
End Class

Public Module Program
    Public Sub Main()

        Dim perrito As Perro = New Perro()
        perrito.nombre = "Chizu"
        perrito.raza = "Pastor Alemán"
        ' Nota: En VB.NET la altura debe ser un número, no un string.
        perrito.altura = 0.7

        Console.WriteLine(perrito.comer("Carne"))

        Dim perrito2 As Perro = New Perro()
        perrito2.nombre = "Lazy"
        perrito2.altura = 0.6

        Console.WriteLine(perrito2.comer("Pollo"))

        Dim perrito3 As Perro = New Perro("Peluchin", "Poodle", 0.5)
        Console.WriteLine(perrito3.comer("Pan"))
        Console.ReadLine()
    End Sub
End Module