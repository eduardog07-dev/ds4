using ExcelDataReader;
using GestiónInventario.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace GestiónInventario.Controllers
{
    [RoutePrefix("api/productos")]
    public class ProductosApiController : ApiController
    {
        [HttpPost]
        [Route("guardar")]
        public IHttpActionResult Guardar(Producto obj)
        {
            if (obj == null) return BadRequest("No se recibieron datos.");
            try
            {
                string conexionString = ConfigurationManager.ConnectionStrings["CadenaInventario"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(conexionString))
                {
                    string query = @"INSERT INTO ProductosIT (NumeroSerie, Identificador, FechaCreacion, Fabricante, Modelo, TipoCI, Hostname, DireccionIP, MacAddress, SistemaOperativo, RamGB, Almacenamiento, Sucursal, Ubicacion, Estado, NumeroActivo, Titulo, CreadoPor) VALUES (@Serie, @Identificador, @FechaCreacion, @Fabricante, @Modelo, @TipoCI, @Hostname, @IP, @MAC, @SO, @RamGB, @Almacenamiento, @Sucursal, @Ubicacion, @Estado, @NumeroActivo, @Titulo, @CreadoPor)";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Serie", obj.Serie ?? "");
                    cmd.Parameters.AddWithValue("@Identificador", obj.Identificador ?? "");
                    cmd.Parameters.AddWithValue("@FechaCreacion", obj.FechaCreacion == DateTime.MinValue ? DateTime.Now : obj.FechaCreacion);
                    cmd.Parameters.AddWithValue("@Fabricante", obj.Fabricante ?? "");
                    cmd.Parameters.AddWithValue("@Modelo", obj.Modelo ?? "");
                    cmd.Parameters.AddWithValue("@TipoCI", obj.TipoCI ?? "");
                    cmd.Parameters.AddWithValue("@Hostname", obj.Hostname ?? "");
                    cmd.Parameters.AddWithValue("@IP", obj.IP ?? "");
                    cmd.Parameters.AddWithValue("@MAC", obj.MAC ?? "");
                    cmd.Parameters.AddWithValue("@SO", obj.SO ?? "");
                    int ramLimpia = 0;
                    if (!string.IsNullOrEmpty(obj.RAM))
                    {
                        string soloNumeros = System.Text.RegularExpressions.Regex.Match(obj.RAM, @"\d+").Value;
                        int.TryParse(soloNumeros, out ramLimpia);
                    }
                    cmd.Parameters.AddWithValue("@RamGB", ramLimpia);
                    cmd.Parameters.AddWithValue("@Almacenamiento", obj.Almacenamiento ?? "");
                    cmd.Parameters.AddWithValue("@Sucursal", obj.Sucursal ?? "");
                    cmd.Parameters.AddWithValue("@Ubicacion", obj.Ubicacion ?? "");
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado ?? "");
                    cmd.Parameters.AddWithValue("@NumeroActivo", obj.NumeroActivo ?? "");
                    cmd.Parameters.AddWithValue("@Titulo", obj.Titulo ?? "");
                    cmd.Parameters.AddWithValue("@CreadoPor", obj.CreadoPor ?? "Admin");
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
                return Ok(new { mensaje = "Guardado correctamente" });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("listar")]
        public IHttpActionResult Listar()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["CadenaInventario"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    string query = "SELECT NumeroSerie, Identificador, Modelo, TipoCI, Sucursal, Ubicacion, Estado FROM ProductosIT";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new Producto { Serie = reader["NumeroSerie"].ToString(), Identificador = reader["Identificador"].ToString(), Modelo = reader["Modelo"].ToString(), TipoCI = reader["TipoCI"].ToString(), Sucursal = reader["Sucursal"].ToString(), Ubicacion = reader["Ubicacion"].ToString(), Estado = reader["Estado"].ToString() });
                    }
                }
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Error en Listar: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("buscar")]
        public IHttpActionResult Buscar(string texto = "", string tipo = "", string estado = "")
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["CadenaInventario"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    string query = @"SELECT NumeroSerie, Identificador, Modelo, TipoCI, Hostname, DireccionIP, Sucursal, Ubicacion, Estado, Titulo, RamGB, Almacenamiento, SistemaOperativo FROM ProductosIT WHERE 1=1";

                    if (!string.IsNullOrEmpty(texto)) query += " AND (NumeroSerie LIKE @t OR Modelo LIKE @t OR Hostname LIKE @t)";
                    if (!string.IsNullOrEmpty(tipo)) query += " AND TipoCI = @tipo";
                    if (!string.IsNullOrEmpty(estado)) query += " AND Estado = @est";

                    SqlCommand cmd = new SqlCommand(query, con);
                    if (!string.IsNullOrEmpty(texto)) cmd.Parameters.AddWithValue("@t", "%" + texto + "%");
                    if (!string.IsNullOrEmpty(tipo)) cmd.Parameters.AddWithValue("@tipo", tipo);
                    if (!string.IsNullOrEmpty(estado)) cmd.Parameters.AddWithValue("@est", estado);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Producto
                        {
                            Serie = reader["NumeroSerie"].ToString(),
                            Identificador = reader["Identificador"].ToString(),
                            Modelo = reader["Modelo"].ToString(),
                            TipoCI = reader["TipoCI"].ToString(),
                            Hostname = reader["Hostname"].ToString(),
                            IP = reader["DireccionIP"].ToString(),
                            Sucursal = reader["Sucursal"].ToString(),
                            Ubicacion = reader["Ubicacion"].ToString(),
                            Estado = reader["Estado"].ToString(),
                            Titulo = reader["Titulo"] != DBNull.Value ? reader["Titulo"].ToString() : "",
                            RAM = reader["RamGB"] != DBNull.Value ? reader["RamGB"].ToString() : "",
                            Almacenamiento = reader["Almacenamiento"] != DBNull.Value ? reader["Almacenamiento"].ToString() : "",
                            SO = reader["SistemaOperativo"] != DBNull.Value ? reader["SistemaOperativo"].ToString() : ""
                        });
                    }
                }
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("exportar")]
        public HttpResponseMessage Exportar(string texto = "", string tipo = "", string estado = "")
        {
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("Serie,Identificador,Modelo,Tipo,Hostname,IP,Sucursal,Ubicacion,Estado,CreadoPor,Fecha");
            try
            {
                string conexionString = ConfigurationManager.ConnectionStrings["CadenaInventario"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(conexionString))
                {
                    string query = @"SELECT NumeroSerie, Identificador, Modelo, TipoCI, Hostname, DireccionIP, Sucursal, Ubicacion, Estado, CreadoPor, FechaCreacion FROM ProductosIT WHERE 1=1";
                    if (!string.IsNullOrEmpty(texto)) query += " AND (NumeroSerie LIKE @Texto OR Modelo LIKE @Texto OR Hostname LIKE @Texto)";
                    if (!string.IsNullOrEmpty(tipo)) query += " AND TipoCI = @Tipo";
                    if (!string.IsNullOrEmpty(estado)) query += " AND Estado = @Estado";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    if (!string.IsNullOrEmpty(texto)) cmd.Parameters.AddWithValue("@Texto", "%" + texto + "%");
                    if (!string.IsNullOrEmpty(tipo)) cmd.Parameters.AddWithValue("@Tipo", tipo);
                    if (!string.IsNullOrEmpty(estado)) cmd.Parameters.AddWithValue("@Estado", estado);
                    conexion.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string s = reader["NumeroSerie"].ToString().Replace(",", " ");
                        string i = reader["Identificador"].ToString().Replace(",", " ");
                        string m = reader["Modelo"].ToString().Replace(",", " ");
                        string t = reader["TipoCI"].ToString().Replace(",", " ");
                        string h = reader["Hostname"].ToString().Replace(",", " ");
                        string ip = reader["DireccionIP"].ToString().Replace(",", " ");
                        string suc = reader["Sucursal"].ToString().Replace(",", " ");
                        string ubi = reader["Ubicacion"].ToString().Replace(",", " ");
                        string est = reader["Estado"].ToString().Replace(",", " ");
                        string c = reader["CreadoPor"].ToString().Replace(",", " ");

                        // CORRECCIÓN: Manejar DBNull para FechaCreacion
                        string f = reader["FechaCreacion"] != DBNull.Value ? Convert.ToDateTime(reader["FechaCreacion"]).ToString("yyyy-MM-dd") : "";

                        csv.AppendLine($"{s},{i},{m},{t},{h},{ip},{suc},{ubi},{est},{c},{f}");
                    }
                }
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(csv.ToString())).ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = $"Reporte_Inventario_{DateTime.Now:yyyyMMdd}.csv" };
                return result;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("importar")]
        public async Task<IHttpActionResult> Importar()
        {
            if (!Request.Content.IsMimeMultipartContent()) return BadRequest("Formato no soportado");

            try
            {
                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);

                if (provider.Contents.Count == 0) return BadRequest("Sin archivo");

                var file = provider.Contents[0];
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                var stream = await file.ReadAsStreamAsync();
                int guardados = 0;

                string connStr = ConfigurationManager.ConnectionStrings["CadenaInventario"].ConnectionString;

                if (filename.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    using (var reader = new StreamReader(stream, Encoding.GetEncoding("iso-8859-1")))
                    {
                        string linea;
                        int fila = 0;
                        using (SqlConnection con = new SqlConnection(connStr))
                        {
                            con.Open();
                            while ((linea = reader.ReadLine()) != null)
                            {
                                fila++;
                                if (fila == 1 && (linea.ToLower().Contains("serie") || linea.ToLower().Contains("modelo"))) continue;
                                if (string.IsNullOrWhiteSpace(linea)) continue;

                                var datos = linea.Split(new[] { ',', ';' });
                                if (datos.Length >= 9)
                                {
                                    InsertarRegistro(con, datos);
                                    guardados++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    // NOTA: Esta sección (ExcelDataReader) causa el error CS0246 si el NuGet no está instalado. 
                    // Si el error persiste, DEBERÁS eliminar esta sección 'else' completa o instalar el paquete.
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();
                        var tabla = result.Tables[0];

                        using (SqlConnection con = new SqlConnection(connStr))
                        {
                            con.Open();
                            for (int i = 0; i < tabla.Rows.Count; i++)
                            {
                                if (i == 0) continue;
                                var row = tabla.Rows[i];

                                List<string> datosLista = new List<string>();
                                for (int j = 0; j < tabla.Columns.Count; j++) datosLista.Add(row[j].ToString());

                                if (datosLista.Count >= 9 && !string.IsNullOrEmpty(datosLista[0]))
                                {
                                    InsertarRegistro(con, datosLista.ToArray());
                                    guardados++;
                                }
                            }
                        }
                    }
                }

                return Ok(new { mensaje = $"Procesados {guardados} registros exitosamente." });
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        // --- MÉTODO HELPER (Necesario para Importar) ---
        private void InsertarRegistro(SqlConnection con, string[] datos)
        {
            string q = @"INSERT INTO ProductosIT (NumeroSerie, Identificador, Modelo, TipoCI, Hostname, DireccionIP, Sucursal, Ubicacion, Estado, FechaCreacion, CreadoPor) 
                         VALUES (@s, @id, @m, @t, @h, @ip, @suc, @ubi, @e, GETDATE(), 'CargaMasiva')";

            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@s", datos[0].Trim());
            cmd.Parameters.AddWithValue("@id", datos.Length > 1 ? datos[1].Trim() : datos[0].Trim());
            cmd.Parameters.AddWithValue("@m", datos.Length > 2 ? datos[2].Trim() : "");
            cmd.Parameters.AddWithValue("@t", datos.Length > 3 ? datos[3].Trim() : "");
            cmd.Parameters.AddWithValue("@h", datos.Length > 4 ? datos[4].Trim() : "");
            cmd.Parameters.AddWithValue("@ip", datos.Length > 5 ? datos[5].Trim() : "");
            cmd.Parameters.AddWithValue("@suc", datos.Length > 6 ? datos[6].Trim() : "");
            cmd.Parameters.AddWithValue("@ubi", datos.Length > 7 ? datos[7].Trim() : "");
            cmd.Parameters.AddWithValue("@e", datos.Length > 8 ? datos[8].Trim() : "Operativo");

            cmd.ExecuteNonQuery();
        }

        // --- MÉTODO ACTUALIZADO PARA LAS TARJETAS DEL DASHBOARD ---
        [HttpGet]
        [Route("resumen")]
        public IHttpActionResult Resumen()
        {
            try
            {
                var listaResumen = new List<object>();
                string connStr = ConfigurationManager.ConnectionStrings["CadenaInventario"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    // Consulta agrupada por TipoCI para las tarjetas
                    string query = @"
                        SELECT TipoCI, COUNT(*) AS Cantidad 
                        FROM ProductosIT 
                        WHERE Estado IN ('Operativo', 'Prestamo') 
                        GROUP BY TipoCI";

                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string tipo = reader["TipoCI"] != DBNull.Value ? reader["TipoCI"].ToString() : "Sin Clasificar";
                        int cantidad = Convert.ToInt32(reader["Cantidad"]);

                        // Lógica de colores y estado de stock (Crítico, Bajo, A Nivel, Mucho Stock)
                        string estadoStock = "A Nivel";
                        string color = "verde";

                        if (cantidad <= 5) { estadoStock = "Crítico"; color = "rojo"; }
                        else if (cantidad > 5 && cantidad <= 20) { estadoStock = "Bajo"; color = "naranja"; }
                        else if (cantidad > 50) { estadoStock = "Mucho Stock"; color = "azul"; }

                        listaResumen.Add(new { Tipo = tipo, Cantidad = cantidad, Estado = estadoStock, Color = color });
                    }
                }
                return Ok(listaResumen); // Devuelve la lista con la estructura que el JS espera
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        [HttpPost]
        [Route("actualizar")]
        public IHttpActionResult Actualizar(Producto obj)
        {
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["CadenaInventario"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    string query = @"UPDATE ProductosIT SET Sucursal=@suc, Ubicacion=@ubi, Estado=@e, Titulo=@tit, Hostname=@h, RamGB=@ram, Almacenamiento=@alm, SistemaOperativo=@so WHERE NumeroSerie=@s";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@s", obj.Serie);
                    cmd.Parameters.AddWithValue("@suc", obj.Sucursal ?? "");
                    cmd.Parameters.AddWithValue("@ubi", obj.Ubicacion ?? "");
                    cmd.Parameters.AddWithValue("@e", obj.Estado ?? "");
                    cmd.Parameters.AddWithValue("@tit", obj.Titulo ?? "");
                    cmd.Parameters.AddWithValue("@h", obj.Hostname ?? "");
                    cmd.Parameters.AddWithValue("@alm", obj.Almacenamiento ?? "");
                    cmd.Parameters.AddWithValue("@so", obj.SO ?? "");
                    int r = 0;
                    if (!string.IsNullOrEmpty(obj.RAM))
                    {
                        int.TryParse(System.Text.RegularExpressions.Regex.Match(obj.RAM, @"\d+").Value, out r);
                    }
                    cmd.Parameters.AddWithValue("@ram", r);
                    con.Open();
                    int filas = cmd.ExecuteNonQuery();
                    if (filas > 0) return Ok(new { mensaje = "Actualizado correctamente" });
                    else return BadRequest("No se encontró el equipo para actualizar.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("eliminar")]
        public IHttpActionResult Eliminar([FromBody] string serie)
        {
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["CadenaInventario"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    string query = "DELETE FROM ProductosIT WHERE NumeroSerie = @s";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@s", serie);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                return Ok(new { mensaje = "Eliminado" });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("eliminarMasivo")]
        public IHttpActionResult EliminarMasivo(List<string> series)
        {
            if (series == null || series.Count == 0) return BadRequest("No seleccionados.");
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["CadenaInventario"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    foreach (var serie in series)
                    {
                        string query = "DELETE FROM ProductosIT WHERE NumeroSerie = @s";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@s", serie);
                        cmd.ExecuteNonQuery();
                    }
                }
                return Ok(new { mensaje = $"Se eliminaron {series.Count} equipos." });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}