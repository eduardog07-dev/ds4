List <int> calificaciones = new List<int> {05,90,70,92,88};
int suma = 0;
foreach (int calificacion in calificaciones)
{
    suma += calificacion;
}
double promedio = suma / (double)calificaciones.Count;
Console.WriteLine($"El promedio de las calificaciones es :(promedio)");
