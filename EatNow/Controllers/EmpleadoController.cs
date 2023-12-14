using EatNow.DAL;
using EatNow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace EatNow.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly RestauranteDAL restauranteDAL;
        private readonly EmpleadoDAL empleadoDAL;
        private readonly PlatoDAL platoDAL;

        public EmpleadoController()
        {
            restauranteDAL = new RestauranteDAL(Conexion.CadenaBBDD);
            empleadoDAL = new EmpleadoDAL(Conexion.CadenaBBDD);
            platoDAL = new PlatoDAL(Conexion.CadenaBBDD);
        }

        // GET: EmpleadoController
        public IActionResult Index()
        {
            if (Request.Cookies["IdEmpleado"] != null)
            {
                ViewBag.IdEmpleado = Request.Cookies["IdEmpleado"];
            }

            return View();
        }

        public IActionResult ListaReservasRestaurante()
        {
            if (Request.Cookies["IdEmpleado"] != null)
            {
                ViewBag.IdEmpleado = Request.Cookies["IdEmpleado"];
            }
            return RedirectToAction("ListReservasRestaurante", "Restaurante");
        }

        public IActionResult DatosRestaurante()
        {
            if (Request.Cookies["IdEmpleado"] != null)
            {
                ViewBag.IdEmpleado = Request.Cookies["IdEmpleado"];
            }
            return RedirectToAction("InfoRestaurante", "Restaurante");
        }

        public IActionResult MenuRestaurante()
        {
            int idEmpleado = int.Parse(Request.Cookies["IdEmpleado"]);
            Empleado empleado = empleadoDAL.GetEmployeeById(idEmpleado);
            Restaurante restaurante = restauranteDAL.GetRestaurantById(empleado.RIdRestaurante);
            List<Plato> platos = platoDAL.GetAllDishesFromRestaurant(restaurante.IdRestaurante);

            ViewBag.IdRestaurante = restaurante.IdRestaurante;

            return View(platos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AñadirPlato(string name, string precio, string URL, int IdRestaurante)
        {

            Plato plato = new Plato { Nombre = name, Precio = double.Parse(precio), URLImagen = URL, RIdRestaurante = IdRestaurante };
            int affectedRows = platoDAL.InsertDish(plato);

            return RedirectToAction("MenuRestaurante");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarPlato(string IdPlato)
        {
            int affectedRows = platoDAL.DeleteDish(int.Parse(IdPlato));

            return RedirectToAction("MenuRestaurante");
        }

        public IActionResult ListEmpleadosRestaurante()
        {
            List<Empleado> empleados = new List<Empleado>();

            return View(empleados);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmpleado()
        {
            return RedirectToAction("ListEmpleadosRestaurante");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarEmpleado()
        {
            return RedirectToAction("ListEmpleadosRestaurante");
        }

        public IActionResult mapaRestauranteEmpleado()
        {
            return View();
        }

        public IActionResult InfoUsuario()
        {
            if (Request.Cookies["IdEmpleado"] != null)
            {
                ViewBag.IdEmpleado = Request.Cookies["IdEmpleado"];
            }

            return View();
        }
    }
}
