using EatNow.DAL;
using EatNow.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EatNow.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestauranteDAL restauranteDAL;
        private readonly ClienteDAL clienteDAL;

        public HomeController()
        {
            restauranteDAL = new RestauranteDAL(Conexion.CadenaBBDD);
            clienteDAL = new ClienteDAL(Conexion.CadenaBBDD);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetCliente(Cliente cliente)
        {
            cliente = clienteDAL.GetClientByEmailPassword(cliente.CorreoElectronico, cliente.Password);

            if (cliente == null)
            {
                TempData["ErrorLoginClientMessage"] = "Tu correo electrónico y/o contrseña son incorrectos";
                return RedirectToAction("login");
            }
            else
            {
                // Guardamos el objeto cliente en el ViewBag
                ViewBag.Cliente = cliente;
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Cliente cliente)
        {
            int affectedRows = clienteDAL.InsertClient(cliente);

            // Si devuelve -1 ha habido un error
            if (affectedRows == -1)
                TempData["ErrorCreatingClient"] = "Ha habido un error al crear tu cuenta, vuelve a intentarlo";
            else if (affectedRows == 1)
                TempData["ClientCreatedMessage"] = "Cuenta creada correctamente!";
                
            return RedirectToAction("login");
        }

        public IActionResult Index()
        {
            List<Restaurante> listRestaurants = new List<Restaurante>();
            listRestaurants = restauranteDAL.GetAllRestaurants();

            return View(listRestaurants);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
