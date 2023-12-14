using EatNow.DAL;
using EatNow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EatNow.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly EmpleadoDAL empleadoDAL;

        public EmpleadoController()
        {
            empleadoDAL = new EmpleadoDAL(Conexion.CadenaBBDD);
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

        public IActionResult ListEmpleadosRestaurante()
        {
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
            empleadoDAL.DeleteEmployee(int.Parse(idEmpleado));
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
