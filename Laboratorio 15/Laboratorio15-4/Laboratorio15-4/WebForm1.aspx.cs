using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Laboratorio15_4
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            double valor1, valor2;

            // Intentamos convertir los valores de los TextBox
            bool exito1 = double.TryParse(txtb1.Text, out valor1);
            bool exito2 = double.TryParse(txtb2.Text, out valor2);

            if (exito1 && exito2)
            {
                // Sumar los valores
                double suma = valor1 + valor2;

                // Mostrar el resultado en el Label
                lblResultado.Text = "El valor sumado es = " + suma.ToString();
            }
            else
            {
                // Mostrar error en el Label si hay un valor inválido
                lblResultado.Text = "Por favor ingrese números válidos en ambos campos.";
            }

        }

    }
}