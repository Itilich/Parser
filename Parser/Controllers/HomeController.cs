using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Parser.Models;
using Parser.Data;
using Parser.Models;
using System.Text.RegularExpressions;
using LinkParsers;

namespace Parser.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ParserContext _context; 

        public HomeController(ILogger<HomeController> logger, ParserContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Appender()
        { 
            return View(new HomeAppenderViewModel());
        }

        [HttpPost]
        public IActionResult Appender(HomeAppenderModel model)
        {
            var viewModel = new HomeAppenderViewModel()
            { 
            };
            _context.addedDatas.Add(new AddedData
            {
                Name = model.ProductName,
                LinkCersanit = model.LinkCersanit,
                LinkVodoparad = model.LinkVodoparad,
            });
            _context.SaveChanges();

            return View(viewModel);
        }

        public IActionResult Delete(int id)
        {
            var variant = _context.addedDatas.FirstOrDefault(x=> x.Id == id);
            if (variant != null)
            {
                _context.addedDatas.Remove(variant);
                _context.SaveChanges();

            }


            return RedirectToAction("Index");


        }

        public async Task LinkDomotex(string name)
        {
            await LinkParsers.LinkParser.LinkDomotex(_context, name);
        }

        public async Task LinkVodoparad(string name)
        {
            await LinkParsers.LinkParser.LinkVodoparad(_context, name);
        }

        public IActionResult DataViewer()
        {
            var data = _context.addedDatas.ToList();
            
            return View(data);
        }

        public IActionResult Results()
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
