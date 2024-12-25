using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Parser.Models;
using Parser.Data;
using Parser.Models;
using System.Text.RegularExpressions;
using LinkParsers;
using Microsoft.EntityFrameworkCore;

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
            // ��������� ������, ��������� �������������, � ���� ������
            var addedData = new AddedData
            {
                Name = model.ProductName,
                LinkDomotex = model.LinkDomotex,
                LinkVodoparad = model.LinkVodoparad
            };

            _context.addedDatas.Add(addedData);
            await _context.SaveChangesAsync();

            // ������ ���� � ������
            var priceDomotex = await LinkParsers.LinkParser.LinkDomotex(_context, model.ProductName);
            var priceVodoparad = await LinkParsers.LinkParser.LinkVodoparad(_context, model.ProductName);

            // ��������� ��������� �������� � ������� PriceLog
            _context.priceLogs.Add(new PriceLog
            {
                ProductId = addedData.Id,
                ProductName = addedData.Name, // ��������� �������� ������
                DateTime = DateTime.Now.ToString(),
                PriceDomotex = priceDomotex ?? 0,
                PriceVodoparad = priceVodoparad ?? 0,
                LinkDomotex = addedData.LinkDomotex,
                LinkVodoparad = addedData.LinkVodoparad
            });

            await _context.SaveChangesAsync();

            return RedirectToAction("Results");
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var variant = await _context.addedDatas.FirstOrDefaultAsync(x => x.Id == id);
                if (variant != null)
                {
                    _context.addedDatas.Remove(variant);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("������� �������: {Id}", id);
                }
                else
                {
                    _logger.LogWarning("������� ��������: ������ � Id {Id} �� �������.", id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "������ ��� �������� ������ � Id {Id}.", id);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Prices(HomeAppenderModel model, int id)
        {
            var addedData = await _context.addedDatas.FirstOrDefaultAsync(x => x.Id == id); 

            var priceDomotex = await LinkParsers.LinkParser.LinkDomotex(_context, model.ProductName);
            var priceVodoparad = await LinkParsers.LinkParser.LinkVodoparad(_context, model.ProductName);

            _context.priceLogs.Add(new PriceLog
            {
                ProductId = addedData.Id,
                ProductName = addedData.Name,
                DateTime = DateTime.Now.ToString(),
                PriceDomotex = Convert.ToDouble(priceDomotex),
                PriceVodoparad = Convert.ToDouble(priceVodoparad),
                LinkDomotex = addedData.LinkDomotex,
                LinkVodoparad = addedData.LinkVodoparad

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
            var priceLogs = await _context.priceLogs.ToListAsync();

            if (!priceLogs.Any())
            {
                _logger.LogWarning("������ ����������� � ������� PriceLog.");
                return View("NoResults"); // ������� �������� "��� ������", ���� �����
            }

            return View(priceLogs);
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
