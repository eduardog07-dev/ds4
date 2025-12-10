using System;
using System.ComponentModel.DataAnnotations;

namespace GestiónInventario.Models
{
    public class Producto
    {
        public int Id
        {
            get; set;
        }

        [Required]
        public string Codigo
        {
            get; set;
        } // Código de barras o referencia

        [Required]
        public string Nombre
        {
            get; set;
        }

        public string Categoria
        {
            get; set;
        } // "Hardware", "Periférico", etc.

        public int StockActual
        {
            get; set;
        }

        public int StockMinimo
        {
            get; set;
        } // Para alertas

        public decimal Precio
        {
            get; set;
        }
    }
}