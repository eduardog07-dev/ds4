using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Parcial2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCvtDecimal_Click(object sender, EventArgs e)
        {
            var bin = txtBB.Text?.Trim();


            if (string.IsNullOrWhiteSpace(bin) || !bin.All(c => c == '0' || c == '1'))
                txtBD.Text = "Entrada inválida";
            else
            {
                txtBD.Text = Convert.ToInt64(bin, 2).ToString();
                SaveConversion("Binario", bin, "Decimal", txtBD.Text);
            }
        }

        private void btnCvtBinario_Click(object sender, EventArgs e)
        {

            var texto = txtBD2.Text?.Trim();
            if (long.TryParse(texto, out long decimalValue) && decimalValue >= 0)
            {
                txtBB2.Text = Convert.ToString(decimalValue, 2);
                SaveConversion("Decimal", texto, "Binario", txtBB2.Text);
            }
            else
                txtBB2.Text = "Entrada inválida";
        }

        private void CvtDecimal2_Click(object sender, EventArgs e)
        {
            var octal = txtBO3.Text?.Trim();

            if (string.IsNullOrWhiteSpace(octal) || !octal.All(c => c >= '0' && c <= '7'))
                txtBD3.Text = "Entrada inválida";
            else
            {
                txtBD3.Text = Convert.ToInt64(octal, 8).ToString();
                SaveConversion("Octal", octal, "Decimal", txtBD3.Text);
            }
        }

        private void CvtOctal_Click(object sender, EventArgs e)
        {
            var texto = txtBD4.Text?.Trim();
            if (long.TryParse(texto, out long decValue) && decValue >= 0)
            {
                txtBO4.Text = Convert.ToString(decValue, 8);
                SaveConversion("Decimal", texto, "Octal", txtBO4.Text);
            }
            else
                txtBO4.Text = "Entrada inválida";
        }

        private void SaveConversion(string tipoOrigen, string valorIngresado, string tipoDestino, string valorConvertido)
        {
            var cs = ConfigurationManager.ConnectionStrings["MiConexion"]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(cs))
                return;

            const string sql = @"
INSERT INTO Conversiones (ValorIngresado, TipoOrigen, ValorConvertido, TipoDestino, FechaRegistro)
VALUES (@ingresado, @origen, @convertido, @destino, GETDATE())";

            using (var conn = new SqlConnection(cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ingresado", (object)valorIngresado ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@origen", (object)tipoOrigen ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@convertido", (object)valorConvertido ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@destino", (object)tipoDestino ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int saved = 0;

                var origen1 = txtBB.Text?.Trim();
                var destino1 = txtBD.Text?.Trim();
                if (!string.IsNullOrWhiteSpace(origen1) && !string.IsNullOrWhiteSpace(destino1) && destino1 != "Entrada inválida")
                {
                    SaveConversion("Binario", origen1, "Decimal", destino1);
                    saved++;
                }

                var origen2 = txtBD2.Text?.Trim();
                var destino2 = txtBB2.Text?.Trim();
                if (!string.IsNullOrWhiteSpace(origen2) && !string.IsNullOrWhiteSpace(destino2) && destino2 != "Entrada inválida")
                {
                    SaveConversion("Decimal", origen2, "Binario", destino2);
                    saved++;
                }

                var origen3 = txtBO3.Text?.Trim();
                var destino3 = txtBD3.Text?.Trim();
                if (!string.IsNullOrWhiteSpace(origen3) && !string.IsNullOrWhiteSpace(destino3) && destino3 != "Entrada inválida")
                {
                    SaveConversion("Octal", origen3, "Decimal", destino3);
                    saved++;
                }

                var origen4 = txtBD4.Text?.Trim();
                var destino4 = txtBO4.Text?.Trim();
                if (!string.IsNullOrWhiteSpace(origen4) && !string.IsNullOrWhiteSpace(destino4) && destino4 != "Entrada inválida")
                {
                    SaveConversion("Decimal", origen4, "Octal", destino4);
                    saved++;
                }

                MessageBox.Show($"{saved} registro(s) guardado(s).", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}