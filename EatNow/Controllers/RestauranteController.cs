using EatNow.DAL;
using EatNow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EatNow.Controllers
{
    public class RestauranteController : Controller
    {
        private readonly RestauranteDAL restauranteDAL;
        private readonly ClienteDAL clienteDAL;
        private readonly EmpleadoDAL empleadoDAL;

        public RestauranteController()
        {
            restauranteDAL = new RestauranteDAL(Conexion.CadenaBBDD);
            clienteDAL = new ClienteDAL(Conexion.CadenaBBDD);
            empleadoDAL = new EmpleadoDAL(Conexion.CadenaBBDD);
        }

        // GET: RestauranteController
        public IActionResult Index()
        {
            if (Request.Cookies["IdCliente"] != null)
                ViewBag.IdCliente = Request.Cookies["IdCliente"];

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int idRestaurante)
        {
            Restaurante restaurante = restauranteDAL.GetRestaurantById(idRestaurante);

            if (restaurante == null)
            {
                List<Restaurante> listRestaurants = new List<Restaurante>();
                listRestaurants = restauranteDAL.GetAllRestaurants();

                TempData["ErrorLoginClientMessage"] = "El restaurante no existe";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Request.Cookies["IdCliente"] != null)
                    ViewBag.IdCliente = Request.Cookies["IdCliente"];

                return View(restaurante);
            }
        }

        public IActionResult MapaRestaurante()
        {
            return View();
        }

        public IActionResult ListReservasRestaurante()
        {
            return View();
        }

        public IActionResult ConfirmacionReserva()
        {
            return View();
        }

        public IActionResult InfoRestaurante()
        {
            int idEmpleado = int.Parse(Request.Cookies["IdEmpleado"]);
            Empleado empleado = empleadoDAL.GetEmployeeById(idEmpleado);
            Restaurante restaurante = restauranteDAL.GetRestaurantById(empleado.RIdRestaurante);

            return View(restaurante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateRestaurante(Restaurante restaurante)
        {
            int affectedRows = restauranteDAL.UpdateRestaurant(restaurante);

            if (affectedRows != -1)
                TempData["ClientUpdatedMessage"] = "Datos actualizados correctamente!";
            else
                TempData["ErrorUpdatingMessage"] = "Ha habido un error al actualizar los datos";

            return RedirectToAction("InfoRestaurante");
        }
    }
}
