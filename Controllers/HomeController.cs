using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using structurizr.Models;
using structurizr.Services;
using System.Threading.Tasks;


namespace structurizr.Controllers;
    
// public class HomeController : Controller
// {
//     private readonly ILogger<HomeController> _logger;

//     public HomeController(ILogger<HomeController> logger)
//     {
//         _logger = logger;
//     }

//     public IActionResult Index()
//     {
//         return View();
//     }

//     public IActionResult Privacy()
//     {
//         return View();
//     }

//     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//     public IActionResult Error()
//     {
//         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//     }
// }

    public class HomeController : Controller
    {
        private readonly DataService _dataService;
        private readonly DiagramService _diagramService;

        public HomeController(DataService dataService, DiagramService diagramService)
        {
            _dataService = dataService;
            _diagramService = diagramService;
        }

        public async Task<IActionResult> Index()
        {
            string url = "https://jsonplaceholder.typicode.com/posts/1"; // Przykładowy endpoint
            string data = await _dataService.GetDataAsync(url);
            string diagramPath = _diagramService.GenerateDiagram(data);
            ViewBag.DiagramPath = diagramPath; // Przekazanie ścieżki diagramu do widoku
            return View();
        }
    }
