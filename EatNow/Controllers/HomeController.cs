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
            // Si ha iniciado sesión le redirigimos a la pantalla de lista de restaurantes
            /*if (Request.Cookies["IdCliente"] != null)
                return RedirectToAction("index");
            // Si ha iniciado sesión como empleado le redirigimos a la vista principal de empleado
            else if (Request.Cookies["IdEmpleado"] != null)
                // TODO: Cambiar la ruta a la que se dirige
                return RedirectToAction("ListReservasRestaurante", "Restaurante");
            else*/
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
                // Guardamos una cookie con el IdCliente
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(1);

                Response.Cookies.Append("IdCliente", cliente.IdCliente.ToString());
                ViewBag.IdCliente = cliente.IdCliente;

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
                // Guardamos una cookie con el IdEmpleado
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(1);

                Response.Cookies.Append("IdEmpleado", empleado.IdEmpleado.ToString());

                // TODO: Cambiar la ruta a la que se dirige
                return RedirectToAction("ListReservasRestaurante", "Restaurante");
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
