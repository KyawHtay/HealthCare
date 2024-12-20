using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HealthCare.Models;
using Microsoft.EntityFrameworkCore;
using HealthCare.DBContext;

namespace HealthCare.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ABCContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ABCContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
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

        public IActionResult DailySummary()
        {
            var today = DateTime.Today;
            var consultations = _dbContext.Consultations
                .Where(c => c.ConsultationDateTime.Date == today)
                .ToList();

            var totalConsultations = consultations.Count;
            var totalFees = consultations.Sum(c => c.FeeCharged);

            ViewBag.TotalConsultations = totalConsultations;
            ViewBag.TotalFees = totalFees;
            return View();
        }

        // GET: Patients/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Patients.Add(patient);
                _dbContext.SaveChanges();
                // After creating a patient, redirect to Index or another appropriate page
                return RedirectToAction("Index");
            }

            // If model is invalid, re-display the form with validation errors
            return View(patient);
        }
    }
}
