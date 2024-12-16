using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Parser.Models;
using Parser.Data;
using Parser.Models;

namespace Parser.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ParserContext _context; 

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Appender(HomeAppenderModel model)
        {
            //var viewModel = new HomeAppenderModel()
            //{
            //};

            //_context.addedDatas.Add(new AddedData
            //{
            //    Name = model.Name,
            //    LinkCersanit = model.LinkCersanit,
            //    LinkVodoparad = model.LinkVodoparad,
            //});
            //_context.SaveChanges();

            return View();
        }

        [HttpGet]
        public IActionResult DataViewer()
        {
            return View();
        }

        public IActionResult Developers()
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
