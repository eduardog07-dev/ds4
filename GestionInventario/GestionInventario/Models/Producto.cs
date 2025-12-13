using System;

namespace GestiónInventario.Models // <--- PON EL NOMBRE REAL DE TU PROYECTO AQUÍ
{
    public class Producto
    {
        // Identificación
        public string Serie { get; set; }
        public string Identificador { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Detalles
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public string TipoCI { get; set; } // Laptop, Desktop, etc.

        // Red
        public string Hostname { get; set; }
        public string IP { get; set; }
        public string MAC { get; set; }
        public string SO { get; set; }

        // Hardware
        public string RAM { get; set; } // Lo recibimos como string ("16GB") y luego lo limpiamos
        public string Almacenamiento { get; set; }

        // Ubicación y Estado
        public string Sucursal { get; set; }
        public string Ubicacion { get; set; }
        public string Estado { get; set; }

        // Extra
        public string NumeroActivo { get; set; }
        public string Titulo { get; set; }
        public string CreadoPor { get; set; }
    }
}