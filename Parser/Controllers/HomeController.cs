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
        public async Task<IActionResult> Appender(HomeAppenderModel model)
        {
            // Добавляем данные, введенные пользователем, в базу данных
            var addedData = new AddedData
            {
                Name = model.ProductName,
                LinkDomotex = model.LinkDomotex,
                LinkVodoparad = model.LinkVodoparad
            };

            _context.addedDatas.Add(addedData);
            await _context.SaveChangesAsync();

            // Парсим цены с сайтов
            var priceDomotex = await LinkParsers.LinkParser.LinkDomotex(_context, model.ProductName);
            var priceVodoparad = await LinkParsers.LinkParser.LinkVodoparad(_context, model.ProductName);

            // Сохраняем результат парсинга в таблицу PriceLog
            _context.priceLogs.Add(new PriceLog
            {
                ProductId = addedData.Id,
                ProductName = addedData.Name, // Сохраняем название товара
                DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                PriceDomotex = priceDomotex ?? 0,
                PriceVodoparad = priceVodoparad ?? 0,
                LinkDomotex = addedData.LinkDomotex,
                LinkVodoparad = addedData.LinkVodoparad
            });

            await _context.SaveChangesAsync();

            return RedirectToAction("Results");
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

        public IActionResult Results()
        {
            var priceLogs = _context.priceLogs.ToList(); // Загружаем данные из базы

            return View(priceLogs); // Передаем список PriceLog в представление
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
