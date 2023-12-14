using EatNow.DAL;
using EatNow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EatNow.Controllers
{
    public class RestauranteController : Controller
    {
        private readonly RestauranteDAL restauranteDAL;
        private readonly ClienteDAL clienteDAL;
        private readonly ReservaDAL reservaDAL;

        public RestauranteController()
        {
            restauranteDAL = new RestauranteDAL(Conexion.CadenaBBDD);
            clienteDAL = new ClienteDAL(Conexion.CadenaBBDD);
            reservaDAL = new ReservaDAL(Conexion.CadenaBBDD);
        }

        // GET: RestauranteController
        public IActionResult Index()
        {
            return View();
        }

        // GET: RestauranteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetRestaurante(Restaurante restaurante)
        {
            restaurante = restauranteDAL.GetRestaurantById(restaurante.IdRestaurante);

            if (restaurante == null)
            {
                List<Restaurante> listRestaurants = new List<Restaurante>();
                listRestaurants = restauranteDAL.GetAllRestaurants();

                TempData["ErrorLoginClientMessage"] = "El restaurante no existe";
                return View(listRestaurants);
            }
            else
            {
                // Guardamos el objeto cliente en el ViewBag
                ViewBag.Restaurante = restaurante;
                return View(restaurante);
            }
        } 

        // GET: RestauranteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RestauranteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult ListReservasRestaurante(int id)
        {
            List<Reserva> reservas = reservaDAL.GetAllReservasRestauranteId(id);

            if (reservas == null)
            {
                List<Reserva> listaReservas = new List<Reserva>();
                listaReservas = reservaDAL.GetAllReservasRestauranteId(id);

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
    }
}
