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

        public async Task<IActionResult> Prices(HomeAppenderModel model, ResultsViewModel model2)
        {
            var priceDomotex = await LinkParsers.LinkParser.LinkDomotex(_context, model.ProductName);
            var priceVodoparad = await LinkParsers.LinkParser.LinkVodoparad(_context, model.ProductName);

            _context.priceLogs.Add(new PriceLog
            {
                ProductId = model2.Id,
                DateTime = DateTime.Now.ToString(),
                PriceDomotex = Convert.ToDouble(priceDomotex),
                PriceVodoparad = Convert.ToDouble(priceVodoparad)
            });

            await _context.SaveChangesAsync();

            return RedirectToAction("Results");
        }

        public IActionResult DataViewer()
        {
            var data = _context.addedDatas.ToList();
            
            return View(data);
        }

        public async Task<IActionResult> Results()
        {
           var priceLogs = _context.priceLogs.ToList();

            var viewModel = new ResultsViewModel
            {
            };

            return View(viewModel);
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
