using EatNow.DAL;
using EatNow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            if (Request.Cookies["IdEmpleado"] != null)
            {
                ViewBag.IdEmpleado = Request.Cookies["IdEmpleado"];
            }

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
            if (Request.Cookies["IdEmpleado"] != null)
            {
                ViewBag.IdEmpleado = Request.Cookies["IdEmpleado"];
            }
            int idEmpleado = int.Parse(Request.Cookies["IdEmpleado"]);
            Empleado empleado = empleadoDAL.GetEmployeeById(idEmpleado);
            
            List<Empleado> empleados = empleadoDAL.GetAllEmployeesExcept(empleado.RIdRestaurante, idEmpleado);

            ViewBag.IdRestaurante = empleado.RIdRestaurante;

            return View(empleados);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]           
        public IActionResult AddEmpleado(string idRestaurante, string dni, string nombre, string apellidos, string email, string password)
        {
            if (Request.Cookies["IdEmpleado"] != null)
            {
                ViewBag.IdEmpleado = Request.Cookies["IdEmpleado"];
            }
            Empleado empleado = new Empleado
            {
                IdEmpleado = int.Parse(idRestaurante),
                DNI = dni,
                Nombre = nombre,
                Apellidos = apellidos,
                CorreoElectronico = email,
                Password = password,
                RIdRestaurante = int.Parse(idRestaurante)
            };
            empleadoDAL.InsertEmployee(empleado);

            return RedirectToAction("ListEmpleadosRestaurante");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarEmpleado(string idEmpleado)
        {
            if (Request.Cookies["IdEmpleado"] != null)
            {
                ViewBag.IdEmpleado = Request.Cookies["IdEmpleado"];
            }
            empleadoDAL.DeleteEmployee(int.Parse(idEmpleado));
            return RedirectToAction("ListEmpleadosRestaurante");
        }

        public IActionResult mapaRestauranteEmpleado()
        {
            if (Request.Cookies["IdEmpleado"] != null)
            {
                ViewBag.IdEmpleado = Request.Cookies["IdEmpleado"];
            }
            return View();
        }

        public IActionResult InfoUsuario()
        {
            if (Request.Cookies["IdEmpleado"] != null)
            {
                ViewBag.IdEmpleado = Request.Cookies["IdEmpleado"];
            }

            Empleado empleado = empleadoDAL.GetEmployeeById(int.Parse(ViewBag.IdEmpleado));

            return View(empleado);
        }
    }
}
