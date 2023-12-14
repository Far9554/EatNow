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
        private readonly ImagenRestauranteDAL imagenRestauranteDAL;
        private readonly ReservaDAL reservaRestauranteDAL;
        private readonly PlatoDAL platoDAL;

        public RestauranteController()
        {
            restauranteDAL = new RestauranteDAL(Conexion.CadenaBBDD);
            clienteDAL = new ClienteDAL(Conexion.CadenaBBDD);
            empleadoDAL = new EmpleadoDAL(Conexion.CadenaBBDD);
            imagenRestauranteDAL = new ImagenRestauranteDAL(Conexion.CadenaBBDD);
            reservaRestauranteDAL = new ReservaDAL(Conexion.CadenaBBDD);
            platoDAL = new PlatoDAL(Conexion.CadenaBBDD);
        }

        // GET: RestauranteController
        public IActionResult Index()
        {

            if (Request.Cookies["IdCliente"] != null)
            {
                ViewBag.IdCliente = Request.Cookies["IdCliente"];
                ViewBag.ImageCliente = clienteDAL.GetClientImage(int.Parse(Request.Cookies["IdCliente"]));
            }

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
                {
                    ViewBag.IdCliente = Request.Cookies["IdCliente"];
                    ViewBag.ImageCliente = clienteDAL.GetClientImage(int.Parse(Request.Cookies["IdCliente"]));
                }

                List<Imagen> images = imagenRestauranteDAL.GetAllRestaurantImages(restaurante.IdRestaurante);
                ViewBag.Images = images;

                List<Plato> platos = platoDAL.GetAllDishesFromRestaurant(restaurante.IdRestaurante);
                ViewBag.Platos = platos;

                return View(restaurante);
            }
        }

        public IActionResult MapaRestaurante()
        {
            if (Request.Cookies["IdCliente"] != null)
            {
                ViewBag.IdCliente = Request.Cookies["IdCliente"];
                ViewBag.ImageCliente = clienteDAL.GetClientImage(int.Parse(Request.Cookies["IdCliente"]));
            }

            return View();
        }

        public IActionResult ListReservasRestaurante(int id)
        {
            if (Request.Cookies["IdEmpleado"] != null)
            {
                ViewBag.IdEmpleado = Request.Cookies["IdEmpleado"];
            }
            List<Reserva> reservas = reservaRestauranteDAL.GetAllReservasRestauranteId(id);

            if (reservas == null)
            {
                List<Reserva> listaReservas = new List<Reserva>();
                listaReservas = reservaRestauranteDAL.GetAllReservasRestauranteId(id);

                TempData["ErrorLoginClientMessage"] = "No tienes reservas";
                return View(listaReservas);
            }
            else
            {
                // Guardamos el objeto cliente en el ViewBag
                //ViewBag.ReservasRestauranteById = reservas;
                return View(reservas);
            }
        }

        public IActionResult ConfirmacionReserva()
        {
            if (Request.Cookies["IdCliente"] != null)
            {
                ViewBag.IdCliente = Request.Cookies["IdCliente"];
                ViewBag.ImageCliente = clienteDAL.GetClientImage(int.Parse(Request.Cookies["IdCliente"]));
            }

            return View();
        }

        public IActionResult InfoRestaurante()
        {
            int idEmpleado = int.Parse(Request.Cookies["IdEmpleado"]);            
            Empleado empleado = empleadoDAL.GetEmployeeById(idEmpleado);

            if (Request.Cookies["IdEmpleado"] != null)
            {
                ViewBag.IdEmpleado = Request.Cookies["IdEmpleado"];
            }

            Restaurante restaurante = restauranteDAL.GetRestaurantById(empleado.RIdRestaurante);

            List<Imagen> images = imagenRestauranteDAL.GetAllRestaurantImages(restaurante.IdRestaurante);
            ViewBag.Images = images;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFotoRestaurante(string idRestaurante, string urlImage)
        {
            Imagen image = new Imagen { RIdRestaurante = int.Parse(idRestaurante), URL = urlImage };
            int affectedRows = imagenRestauranteDAL.InsertRestaurantImage(image);
            return RedirectToAction("InfoRestaurante");
        }

        public IActionResult EliminarFotoRestaurante(string idImage)
        {

            int affectedRows = imagenRestauranteDAL.DeleteImageFromRestaurant(int.Parse(idImage));
            return RedirectToAction("InfoRestaurante");
        }
    }
}
