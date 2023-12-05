using EatNow.DAL;
using EatNow.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EatNow.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestauranteDAL restauranteDAL;

        public HomeController()
        {
            restauranteDAL = new RestauranteDAL(Conexion.CadenaBBDD);
        }

        public IActionResult Login()
        {
            return View();
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
