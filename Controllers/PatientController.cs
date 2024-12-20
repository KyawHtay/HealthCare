using HealthCare.DBContext; // Replace with your actual namespace for the DbContext
using HealthCare.Models;    // Replace with your actual namespace for the Patient model
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ABCContext _context;

        public PatientsController(ABCContext context)
        {
            _context = context;
        }

       
        // OPTIONAL: A listing of patients (Index action)
        // GET: Patients
        public IActionResult Index()
        {
            var patients = _context.Patients.ToList();
            return View(patients);
        }
        // GET: Patients/Create
        [HttpGet]
        public IActionResult Create()
        {
            // This simply returns the view that contains the form
            return View();
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // Helps protect against Cross-Site Request Forgery attacks
        public IActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Patients.Add(patient);
                _context.SaveChanges(); // Save the new patient to the database

                // Redirect back to a list view or details view after successful creation
                return RedirectToAction("Index");
            }

            // If the model state is not valid, re-display the form with validation errors
            return View(patient);
        }

        public IActionResult Details(int id)
        {
            var patient = _context.Patients
                .Include(p => p.Consultations) // If you want to show consultations too
                .FirstOrDefault(p => p.PatientId == id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Patient patient)
        {
            if (id != patient.PatientId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update the ModifiedDate field if desired
                    patient.ModifiedDate = DateTime.Now;
                    _context.Update(patient);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // If the patient no longer exists in the database, return NotFound
                    if (!_context.Patients.Any(p => p.PatientId == patient.PatientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; // some other concurrency issue occurred
                    }
                }

                return RedirectToAction("Index");
            }

            // If model validation fails, re-display the form with the current patient data
            return View(patient);
        }

    }
}
