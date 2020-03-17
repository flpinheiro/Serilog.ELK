using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.ELK.Models;

namespace Serilog.ELK.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDiagnosticContext _diagnosticContext;

        public HomeController(ILogger<HomeController> logger, IDiagnosticContext diagnosticContext)
        {
            _logger = logger;
            _diagnosticContext = diagnosticContext ?? throw new ArgumentNullException(nameof(diagnosticContext));
        }

        public IActionResult Index()
        {
            Log.Information("The global logger has been configured");
            _diagnosticContext.Set("CatalogLoadTime", 1423);
            _logger.Log(LogLevel.Error, "CatalogLoadTime");
            return View();
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
