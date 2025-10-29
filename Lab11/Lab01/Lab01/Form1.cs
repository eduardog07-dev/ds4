using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab01
{
    // Todo este archivo es tu clase Form1
    public partial class Form1 : Form
    {
        // Este es el CONSTRUCTOR.
        // Se ejecuta UNA SOLA VEZ cuando la ventana se crea.
        public Form1()
        {
            InitializeComponent();
        } // Aquí termina el constructor.

        // Este es el MANEJADOR DE EVENTOS del botón.
        // Se ejecuta CADA VEZ que se hace clic en el botón.
        // Debe estar aquí, fuera del constructor pero dentro de la clase Form1.
        private void btn_Click(object sender, EventArgs e)
        {
            lblMundo.Text = "Hola,Mundo";
        }
    } // Aquí termina la clase Form1.
}