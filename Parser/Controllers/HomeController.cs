using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Parser.Models;
using Parser.Data;
using Parser.Models;
using System.Text.RegularExpressions;

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
            var variant = _context.AddedData.FirstOrDefault(x=> x.Id == id));
            if (variant != null)
            {
                _context.AddedData.Remove(variant);
                _context.SaveChanges();

            }


            return RedirectToAction("Index");


        }

        public async Task LinkDomotex(string name)
        {
            //'VALUE_VAT':'(....)'//    
            string link = "";
            var data = _context.addedDatas.FirstOrDefault(x => x.Name == name);
            if (data != null)
            {
                link = data.LinkCersanit;
            }
            var httpClient = new HttpClient();
            var LinkDomotex = await httpClient.GetAsync($"{link}");
            var LinkDomotexBody = await LinkDomotex.Content.ReadAsStringAsync();

            Regex regex = new Regex(@"'VALUE_VAT':'(.+)'");

            //var priceExist = regex.IsMatch(LinkDomotexBody); //проверка наличия цены

            var price = regex.Match(LinkDomotexBody);

        }

        public async Task LinkVodoparad(string name)
        {
            //data-price="(....)"//    
            string link = "";
            var data = _context.addedDatas.FirstOrDefault(x => x.Name == name);
            if (data != null)
            {
                link = data.LinkVodoparad;
            }
            var httpClient = new HttpClient();
            var LinkVodoparad = await httpClient.GetAsync($"{link}");
            var LinkVodoparadBody = await LinkVodoparad.Content.ReadAsStringAsync();

            Regex regex = new Regex(@"data-price=(.+)");

            //var priceExist = regex.IsMatch(LinkVodoparadBody); //проверка наличия цены

            var price = regex.Match(LinkVodoparadBody);

        }

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
