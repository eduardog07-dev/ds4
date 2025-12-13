using GestiónInventario.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
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
        // --- 1. MÉTODO GUARDAR (INDIVIDUAL) ---
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
                    string query = @"
                        INSERT INTO ProductosIT 
                        (NumeroSerie, Identificador, FechaCreacion, Fabricante, Modelo, TipoCI, 
                         Hostname, DireccionIP, MacAddress, SistemaOperativo, 
                         RamGB, Almacenamiento, Sucursal, Ubicacion, Estado, 
                         NumeroActivo, Titulo, CreadoPor) 
                        VALUES 
                        (@Serie, @Identificador, @FechaCreacion, @Fabricante, @Modelo, @TipoCI, 
                         @Hostname, @IP, @MAC, @SO, 
                         @RamGB, @Almacenamiento, @Sucursal, @Ubicacion, @Estado, 
                         @NumeroActivo, @Titulo, @CreadoPor)";

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

        // --- 2. MÉTODO LISTAR (PARA EL INDEX) ---
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
                        lista.Add(new Producto
                        {
                            Serie = reader["NumeroSerie"].ToString(),
                            Identificador = reader["Identificador"].ToString(),
                            Modelo = reader["Modelo"].ToString(),
                            TipoCI = reader["TipoCI"].ToString(),
                            Sucursal = reader["Sucursal"].ToString(),
                            Ubicacion = reader["Ubicacion"].ToString(),
                            Estado = reader["Estado"].ToString()
                        });
                    }
                }
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Error en Listar: " + ex.Message);
            }
        }

        // --- 3. MÉTODO BUSCAR (PARA REPORTES) ---
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
                    string query = @"SELECT NumeroSerie, Identificador, Modelo, TipoCI, Hostname, DireccionIP, Sucursal, Ubicacion, Estado FROM ProductosIT WHERE 1=1";

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
                            Estado = reader["Estado"].ToString()
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

        // --- 4. MÉTODO EXPORTAR (DESCARGAR CSV) ---
        [HttpGet]
        [Route("exportar")]
        public HttpResponseMessage Exportar(string texto = "", string tipo = "", string estado = "")
        {
            StringBuilder csv = new StringBuilder();
            // Encabezados del archivo CSV
            csv.AppendLine("Serie,Identificador,Modelo,Tipo,Hostname,IP,Sucursal,Ubicacion,Estado,CreadoPor,Fecha");

            try
            {
                string conexionString = ConfigurationManager.ConnectionStrings["CadenaInventario"].ConnectionString;

                using (SqlConnection conexion = new SqlConnection(conexionString))
                {
                    string query = @"
                        SELECT NumeroSerie, Identificador, Modelo, TipoCI, Hostname, DireccionIP, 
                               Sucursal, Ubicacion, Estado, CreadoPor, FechaCreacion 
                        FROM ProductosIT 
                        WHERE 1=1";

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
                        // Limpieza de comas para no romper el CSV
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
                        string f = Convert.ToDateTime(reader["FechaCreacion"]).ToString("yyyy-MM-dd");

                        csv.AppendLine($"{s},{i},{m},{t},{h},{ip},{suc},{ubi},{est},{c},{f}");
                    }
                }

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                byte[] csvBytes = Encoding.UTF8.GetBytes(csv.ToString());
                byte[] bom = Encoding.UTF8.GetPreamble();
                List<byte> finalBytes = new List<byte>();
                finalBytes.AddRange(bom);
                finalBytes.AddRange(csvBytes);

                result.Content = new ByteArrayContent(finalBytes.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = $"Reporte_Inventario_{DateTime.Now:yyyyMMdd}.csv" };

                return result;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // --- 5. MÉTODO IMPORTAR (CORREGIDO PARA TU CSV DE 11 COLUMNAS) ---
        [HttpPost]
        [Route("importar")]
        public async Task<IHttpActionResult> Importar()
        {
            if (!Request.Content.IsMimeMultipartContent())
                return BadRequest("Formato no soportado");

            try
            {
                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);

                var archivo = provider.Contents[0];
                var contenidoStream = await archivo.ReadAsStreamAsync();

                int guardados = 0;
                string errores = "";

                using (var reader = new StreamReader(contenidoStream))
                {
                    string linea;
                    int fila = 0;

                    string connStr = ConfigurationManager.ConnectionStrings["CadenaInventario"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        con.Open();

                        while ((linea = reader.ReadLine()) != null)
                        {
                            fila++;
                            // Saltamos la cabecera si la detectamos
                            if (fila == 1 && (linea.ToLower().Contains("serie") || linea.ToLower().Contains("modelo"))) continue;
                            if (string.IsNullOrWhiteSpace(linea)) continue;

                            // Separamos por comas (CSV estándar)
                            var datos = linea.Split(',');

                            // TU ARCHIVO TIENE 11 COLUMNAS:
                            // 0:Serie, 1:Id, 2:Modelo, 3:Tipo, 4:Host, 5:IP, 6:Sucursal, 7:Ubic, 8:Estado, 9:Creador, 10:Fecha

                            if (datos.Length >= 9) // Validamos que tenga al menos los datos principales
                            {
                                try
                                {
                                    string q = @"
                                        INSERT INTO ProductosIT 
                                        (NumeroSerie, Identificador, Modelo, TipoCI, Hostname, DireccionIP, Sucursal, Ubicacion, Estado, CreadoPor, FechaCreacion) 
                                        VALUES 
                                        (@s, @id, @m, @t, @h, @ip, @suc, @ubi, @e, @cre, @fec)";

                                    SqlCommand cmd = new SqlCommand(q, con);

                                    // Mapeo corregido según tu archivo:
                                    cmd.Parameters.AddWithValue("@s", datos[0].Trim());   // Serie
                                    cmd.Parameters.AddWithValue("@id", datos[1].Trim());  // Identificador
                                    cmd.Parameters.AddWithValue("@m", datos[2].Trim());   // Modelo
                                    cmd.Parameters.AddWithValue("@t", datos[3].Trim());   // Tipo
                                    cmd.Parameters.AddWithValue("@h", datos[4].Trim());   // Hostname
                                    cmd.Parameters.AddWithValue("@ip", datos[5].Trim());  // IP
                                    cmd.Parameters.AddWithValue("@suc", datos[6].Trim()); // Sucursal
                                    cmd.Parameters.AddWithValue("@ubi", datos[7].Trim()); // Ubicacion
                                    cmd.Parameters.AddWithValue("@e", datos[8].Trim());   // Estado

                                    // Datos opcionales (Creador y Fecha)
                                    cmd.Parameters.AddWithValue("@cre", datos.Length > 9 ? datos[9].Trim() : "CargaMasiva");

                                    DateTime fechaCarga;
                                    if (datos.Length > 10 && DateTime.TryParse(datos[10], out fechaCarga))
                                        cmd.Parameters.AddWithValue("@fec", fechaCarga);
                                    else
                                        cmd.Parameters.AddWithValue("@fec", DateTime.Now);

                                    cmd.ExecuteNonQuery();
                                    guardados++;
                                }
                                catch (Exception exSql)
                                {
                                    // Guardamos el error pero seguimos con la siguiente fila
                                    errores += $"Fila {fila}: {exSql.Message} | ";
                                }
                            }
                        }
                    }
                }

                return Ok(new { mensaje = $"Proceso terminado. Guardados: {guardados}. {errores}" });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        // --- 6. NUEVO: MÉTODO PARA RESUMEN DE STOCK (SEMÁFORO) ---
        [HttpGet]
        [Route("resumen")]
        public IHttpActionResult ResumenStock()
        {
            try
            {
                var resumen = new List<object>();
                string connStr = ConfigurationManager.ConnectionStrings["CadenaInventario"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    // Agrupamos por Modelo y contamos
                    // OJO: Si prefieres agrupar por 'TipoCI' (Laptop, Desktop...), cambia 'Modelo' por 'TipoCI' en la consulta
                    string query = @"
                        SELECT Modelo, COUNT(*) as Cantidad 
                        FROM ProductosIT 
                        WHERE Estado = 'Operativo' -- Solo contamos los que sirven
                        GROUP BY Modelo 
                        ORDER BY Cantidad DESC";

                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string modelo = reader["Modelo"].ToString();
                        int cantidad = Convert.ToInt32(reader["Cantidad"]);

                        // Lógica del Semáforo (Puedes ajustar estos números)
                        string estadoStock = "Normal";
                        string color = "verde"; // Por defecto

                        if (cantidad <= 5)
                        {
                            estadoStock = "Crítico";
                            color = "rojo";
                        }
                        else if (cantidad > 5 && cantidad <= 15)
                        {
                            estadoStock = "Bajo";
                            color = "naranja";
                        }
                        else if (cantidad > 15 && cantidad <= 50)
                        {
                            estadoStock = "Ideal";
                            color = "verde";
                        }
                        else if (cantidad > 50)
                        {
                            estadoStock = "Exceso";
                            color = "azul";
                        }

                        resumen.Add(new { Modelo = modelo, Cantidad = cantidad, Estado = estadoStock, Color = color });
                    }
                }
                return Ok(resumen);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}