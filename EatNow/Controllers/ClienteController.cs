using EatNow.DAL;
using EatNow.Models;
using Microsoft.AspNetCore.Mvc;

namespace EatNow.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteDAL clienteDAL;
        public ClienteController()
        {
            clienteDAL = new ClienteDAL(Conexion.CadenaBBDD);            
        }

        public IActionResult InfoUsuario()
        {
            int idCliente = int.Parse(Request.Cookies["IdCliente"]);
            Cliente cliente = clienteDAL.GetClientById(idCliente);

            if (Request.Cookies["IdCliente"] != null)
            {
                ViewBag.IdCliente = Request.Cookies["IdCliente"];
                ViewBag.ImageCliente = clienteDAL.GetClientImage(int.Parse(Request.Cookies["IdCliente"]));
            }

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateClient(Cliente cliente)
        {
            cliente.IdCliente = int.Parse(Request.Cookies["IdCliente"]);
            int affectedRows = clienteDAL.UpdateClient(cliente);

            if (affectedRows != -1)
                TempData["ClientUpdatedMessage"] = "Datos actualizados correctamente!";
            else
                TempData["ErrorUpdatingMessage"] = "Ha habido un error al actualizar los datos";

            return RedirectToAction("InfoUsuario");
        }
    }
}
