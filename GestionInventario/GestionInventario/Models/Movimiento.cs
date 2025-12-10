using System;
using System.ComponentModel.DataAnnotations;

namespace GestiónInventario.Models
{
    public class Movimiento
    {
        public int Id
        {
            get; set;
        }

        public int ProductoId
        {
            get; set;
        } // Relación con el producto

        [Required]
        public string Tipo
        {
            get; set;
        } // "Entrada" o "Salida"

        public int Cantidad
        {
            get; set;
        }

        public DateTime Fecha
        {
            get; set;
        }

        public string Observacion
        {
            get; set;
        }
    }
}