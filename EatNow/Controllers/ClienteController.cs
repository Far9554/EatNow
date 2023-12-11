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
            
            return View(cliente);
        }
    }
}
