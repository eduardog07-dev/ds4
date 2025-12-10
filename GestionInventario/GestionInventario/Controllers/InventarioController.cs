using System.Web.Mvc;

namespace GestiónInventario.Controllers
{
    public class InventarioController : Controller
    {
        // GET: Inventario
        public ActionResult Index()
        {
            return View();
        }
        // GET: Inventario/Consultas
        public ActionResult Consultas()
        {
            return View();
        }
        // GET: Inventario/NuevoProducto
        public ActionResult NuevoProducto()
        {
            return View();
        }
    }
}