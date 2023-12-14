using EatNow.DAL;
using EatNow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EatNow.Controllers
{
    public class EmpleadoController : Controller
    {
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
            return RedirectToAction("ListReservasRestaurante", "Restaurante");
        }

        public IActionResult DatosRestaurante()
        {
            return RedirectToAction("InfoRestaurante", "Restaurante");
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

        public IActionResult InfoUsuario()
        {
            if (Request.Cookies["IdEmpleado"] != null)
            {
                ViewBag.IdCliente = Request.Cookies["IdEmpleado"];
            }

            return View();
        }
    }
}
