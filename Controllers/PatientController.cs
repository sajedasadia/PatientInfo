using Microsoft.AspNetCore.Mvc;
using PatientInfo.Models;
using PatientInfo.Repositories;

namespace PatientInfo.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientRepository _repository;

        // Inject the repository into the controller
        public PatientController(IPatientRepository repository)
        {
            _repository = repository;
        }

        // GET: /Patient/
        public async Task<IActionResult> Index()
        {
            var patients = await _repository.GetAllPatientsAsync();
            return View(patients);
        }

        // GET: /Patient/AddEdit/5 or /Patient/AddEdit (for new patient)
        [HttpGet]
        public async Task<IActionResult> AddEdit(int? id)
        {
            if (id.HasValue) // Edit existing patient
            {
                var patient = await _repository.GetPatientByIdAsync(id.Value);
                if (patient == null)
                {
                    return NotFound(); // Return 404 if patient not found
                }
                return View(patient);
            }

            // Add new patient
            return View(new Patient());
        }

        // Alternative method (unnecessary to add both, but included for your reference)
        // This is a custom approach you mentioned, but it's redundant since the AddEdit logic is already covered in the above method.
        public IActionResult AddEdit(int id = 0)
        {
            Patient patient = id == 0
                ? new Patient()
                : _repository.GetPatientByIdAsync(id).Result;  // Using async .Result (caution in production code)

            return View(patient);
        }

        // POST: /Patient/AddEdit
        [HttpPost]
        public async Task<IActionResult> AddEdit(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return View(patient); // Return to the form if validation fails
            }

            if (patient.PatientId == 0) // New patient
            {
                await _repository.AddPatientAsync(patient);
            }
            else // Update existing patient
            {
                await _repository.UpdatePatientAsync(patient);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Patient/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _repository.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound(); // Return 404 if patient not found
            }

            return View(patient);
        }

        // POST: /Patient/DeleteConfirmed
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeletePatientAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
