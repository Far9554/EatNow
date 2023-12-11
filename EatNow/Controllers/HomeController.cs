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
        private readonly EmpleadoDAL empleadoDAL;

        public HomeController()
        {
            restauranteDAL = new RestauranteDAL(Conexion.CadenaBBDD);
            clienteDAL = new ClienteDAL(Conexion.CadenaBBDD);
            empleadoDAL = new EmpleadoDAL(Conexion.CadenaBBDD);
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
                TempData["ErrorLoginMessage"] = "Tu correo electrónico y/o contrseña son incorrectos";
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetEmpleado(string email, string password)
        {
            Empleado empleado = empleadoDAL.GetEmployeeByEmailPassword(email, password);
            
            if (empleado == null)
            {
                TempData["ErrorLoginMessage"] = "Tu correo electrónico y/o contrseña son incorrectos";
                return RedirectToAction("login");
            }
            else
            {
                // Guardamos el objeto empleado en el ViewBag
                ViewBag.Empleado = empleado;
                // TODO: Cambiar la ruta a la que se dirige
                return RedirectToAction("index");
            }
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
