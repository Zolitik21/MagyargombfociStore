using MagyargombfociStore.Models;
using MagyargombfociStore.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MagyargombfociStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _logger = logger;
            _homeRepository = homeRepository;
        }

        public async Task< IActionResult> Index(string sterm="", int categoryId=0)
        {

            

            IEnumerable<Product> buttonfootballs= await _homeRepository.GetButtonFootballs(sterm,categoryId);
            IEnumerable<Categories> categories = await _homeRepository.Categories();
            ButtonFootballDisplayModel buttonfootballModel = new ButtonFootballDisplayModel()
            {
                Products=buttonfootballs,
                Categories=categories,
                STerm=sterm,
                CategoryId=categoryId

            };
            return View(buttonfootballModel);
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