using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProyectoCalculadora
{
    public partial class Form1 : Form
    {
        private double valor1 = 0, valor2 = 0;
        private string operacion = "";
        private bool esOperacionUnaria = false;

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i <= 9; i++)
                Controls["button" + i].Click += numeroClick;

            string[] botonesOperaciones = { "Suma", "Resta", "Multiplicacion", "Division", "Seno", "Coseno", "Raiz", "Borrar", "Porcentaje", "C", "CE", "Elevado", "Igual", "Punto" };
            foreach (string boton in botonesOperaciones)
                Controls["button" + boton].Click += botonClick;
        }

        private void botonClick(object? sender, EventArgs e)
        {
            if (sender is Button boton)
            {
                switch (boton.Name)
                {
                    case "buttonIgual":
                        calcularResultado();
                        break;
                    case "buttonBorrar":
                        if (txtOperacion.Text.Length > 0)
                            txtOperacion.Text = txtOperacion.Text.Remove(txtOperacion.Text.Length - 1);
                        break;
                    case "buttonC":
                    case "buttonCE":
                        txtOperacion.Text = "";
                        txtResultado.Text = "";
                        break;
                    case "buttonSeno":
                    case "buttonCoseno":
                    case "buttonRaiz":
                    case "buttonPorcentaje":
                    case "buttonElevado":
                        agregarOperacionUnaria(boton);
                        break;
                    case "buttonResta":
                        manejarRestaONegativo();
                        break;
                    default:
                        operacionClick(boton);
                        break;
                }
            }
        }

        private void numeroClick(object sender, EventArgs e)
        {
            if (sender is Button boton)
            {
                txtOperacion.Text += boton.Text;
            }
        }

        private void operacionClick(Button boton)
        {
            if (!string.IsNullOrEmpty(txtOperacion.Text))
            {
                valor1 = double.Parse(txtOperacion.Text);
                operacion = boton.Name switch
                {
                    "buttonSuma" => "+",
                    "buttonResta" => "-",
                    "buttonMultiplicacion" => "*",
                    "buttonDivision" => "/",
                    _ => operacion
                };
                txtOperacion.Text += " " + operacion + " ";
                txtResultado.Text = "";
            }
        }

        private void calcularResultado()
        {
            if (esOperacionUnaria)
            {
                operarUnario();
            }
            else
            {
                string[] partes = txtOperacion.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (partes.Length >= 3)
                {
                    valor1 = double.Parse(partes[0]);
                    operacion = partes[1];
                    valor2 = double.Parse(partes[2]);

                    double resultado = operacion switch
                    {
                        "+" => valor1 + valor2,
                        "-" => valor1 - valor2,
                        "*" => valor1 * valor2,
                        "/" when valor2 != 0 => valor1 / valor2,
                        _ => 0
                    };

                    txtResultado.Text = resultado.ToString();
                }
            }
        }

        private void agregarOperacionUnaria(Button boton)
        {
            if (double.TryParse(txtOperacion.Text, out valor1))
            {
                string operacionUnaria = boton.Name switch
                {
                    "buttonSeno" => "seno",
                    "buttonCoseno" => "cos",
                    "buttonRaiz" => "raíz",
                    "buttonPorcentaje" => "porcentaje",
                    "buttonElevado" => "elevado",
                    _ => ""
                };

                txtOperacion.Text = $"{operacionUnaria}({txtOperacion.Text})";
                operacion = operacionUnaria; // Guardar la operación para usarla al presionar igual
                esOperacionUnaria = true;
            }
        }

        private void operarUnario()
        {
            if (double.TryParse(txtOperacion.Text.Trim(new[] { 's', 'e', 'n', 'o', 'c', 'r', 'í', 'a', 'z', '(', ')', ' ' }), out valor1))
            {
                double resultado = operacion switch
                {
                    "seno" => Math.Sin(valor1),
                    "cos" => Math.Cos(valor1),
                    "raíz" => Math.Sqrt(valor1),
                    "porcentaje" => valor1 / 100,
                    "elevado" => Math.Pow(valor1, 2),
                    _ => 0
                };

                txtResultado.Text = resultado.ToString();
                esOperacionUnaria = false;
            }
        }

        private void manejarRestaONegativo()
        {
            if (string.IsNullOrEmpty(txtOperacion.Text) || (txtOperacion.Text.Length == 1 && txtOperacion.Text == "-"))
            {
                txtOperacion.Text = "-";
            }
            else if (!txtOperacion.Text.EndsWith(" - ") && !txtOperacion.Text.Contains(" "))
            {
                txtOperacion.Text += " - ";
            }
        }

     

        private void button3_Click(object sender, EventArgs e) { }
        private void button10_Click(object sender, EventArgs e) { }
        private void button6_Click(object sender, EventArgs e) { }
        private void button11_Click(object sender, EventArgs e) { }
        private void button12_Click(object sender, EventArgs e) { }
        private void button18_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void button15_Click(object sender, EventArgs e) { }
        private void button21_Click(object sender, EventArgs e) { }
        private void button23_Click(object sender, EventArgs e) { }
        private void button24_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
    }
}