using DotNetCore.Helpers;
using DotNetCore.Models;
using DotNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DotNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataService _dataService;
        private readonly INumberServiceTransient _serviceTransient;
        private readonly INumberServiceScoped _serviceScoped;
        private readonly INumberServiceSingleton _serviceSingleton;

        // Injecting dependencies in Controller through Constructor
        public HomeController(IDataService dataService, INumberServiceTransient serviceTransient, INumberServiceScoped serviceScoped, INumberServiceSingleton serviceSingleton)
        {
            _dataService = dataService;
            _serviceTransient = serviceTransient;
            _serviceScoped = serviceScoped;
            _serviceSingleton = serviceSingleton;
        }

        public IActionResult Index([FromServices] INumberServiceTransient serviceTransient, [FromServices] INumberServiceScoped serviceScoped, [FromServices] INumberServiceSingleton serviceSingleton)
        {
            ViewBag.Message = _dataService.GetData();
            ViewBag.NumberTransient1 = _serviceTransient.GetNumber();
            ViewBag.NumberTransient2 = serviceTransient.GetNumber();

            ViewBag.NumberScoped1 = _serviceScoped.GetNumber();
            ViewBag.NumberScoped2 = serviceScoped.GetNumber();

            ViewBag.NumberSingleton1 = _serviceSingleton.GetNumber();
            ViewBag.NumberSingleton2 = serviceSingleton.GetNumber();

            INumberHelper numberHelper = (INumberHelper)HttpContext.RequestServices.GetService(typeof(INumberHelper));
            NumberHelper helper = new NumberHelper(numberHelper);
            ViewBag.Number = helper.GetNumber();
            return View();
        }

        public IActionResult Privacy()
        {
            throw new NullReferenceException();
            //return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
