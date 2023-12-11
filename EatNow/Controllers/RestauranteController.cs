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

        public RestauranteController()
        {
            restauranteDAL = new RestauranteDAL(Conexion.CadenaBBDD);
            clienteDAL = new ClienteDAL(Conexion.CadenaBBDD);
        }

        // GET: RestauranteController
        public IActionResult Index()
        {
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
                return RedirectToAction("Home/Index");
            }
            else
            {
                // Guardamos el objeto cliente en el ViewBag
                ViewBag.Restaurante = restaurante;
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
    }
}
