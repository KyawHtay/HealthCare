using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HealthCare.Controllers
{
    [Route("[controller]")]
    public class ReportController : Controller
    {
        private readonly ILogger<ReportController> _logger;
        private readonly ABCContext _context;

        public ReportController(ILogger<ReportController> logger, ABCContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MonthlyReport(int? year, int? month)
        {
            int reportYear = year ?? DateTime.Today.Year;
            int reportMonth = month ?? DateTime.Today.Month;

            var monthlyConsultations = _context.Consultations
                .Include(c => c.Patient)
                .Where(c => c.ConsultationDateTime.Year == reportYear
                            && c.ConsultationDateTime.Month == reportMonth)
                .ToList();


            return View(monthlyConsultations);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public IActionResult DailyReport(DateTime? date)
        {
            var reportDate = date ?? DateTime.Today;

            var dailyConsultations = _context.Consultations
                .Include(c => c.Patient)
                .Where(c => c.ConsultationDateTime.Date == reportDate)
                .ToList();

            var totalConsultations = dailyConsultations.Count;
            var totalFees = dailyConsultations.Sum(c => c.FeeCharged);

            // Age Group Calculation Example:
            // Calculate patient's age and group them into age brackets
            var ageGroups = dailyConsultations
                .Select(c =>
                {
                    var age = DateTime.Today.Year - c.Patient.DOB.Year;
                    if (c.Patient.DOB.Date > DateTime.Today.AddYears(-age)) age--;
                    return age;
                })
                .GroupBy(age => GetAgeGroup(age))
                .Select(g => new { AgeGroup = g.Key, Count = g.Count() })
                .ToList();

            ViewBag.ReportDate = reportDate;
            ViewBag.TotalConsultations = totalConsultations;
            ViewBag.TotalFees = totalFees;
            ViewBag.AgeGroups = ageGroups;

            return View();
        }

        private string GetAgeGroup(int age)
        {
            if (age < 18) return "Under 18";
            else if (age < 30) return "18-29";
            else if (age < 45) return "30-44";
            else if (age < 60) return "45-59";
            else return "60+";
        }



    }
}