using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Patient.Data;
using WebApp_Patient.Models;

namespace Patient_WebApp.Controllers
{
    public class PatientReferencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientReferencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PatientReferences
        public async Task<IActionResult> Index(int? patientId)
        {
            var query = _context.PatientReferences.Include(p => p.Patient).AsQueryable();

            if (patientId.HasValue)
            {
                query = query.Where(r => r.PatientId == patientId.Value);
            }

            return View(await query.ToListAsync());
        }

        // GET: PatientReferences/Create
        [HttpGet]
        public IActionResult Create(int patientId)
        {
            ViewBag.PatientId = patientId;
            return View();
        }

        // POST: PatientReferences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientReference reference)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PatientId = reference.PatientId;
                return View(reference);
            }

            _context.PatientReferences.Add(reference);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { patientId = reference.PatientId });
        }

        // GET: PatientReferences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var reference = await _context.PatientReferences.FindAsync(id);
            if (reference == null) return NotFound();

            return View(reference);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatientReference reference)
        {
            if (id != reference.ReferenceId) return NotFound();

            if (!ModelState.IsValid) return View(reference);

            try
            {
                _context.Update(reference);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.PatientReferences.Any(e => e.ReferenceId == reference.ReferenceId))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction("Index", new { patientId = reference.PatientId });
        }

        // GET: PatientReferences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var reference = await _context.PatientReferences
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(r => r.ReferenceId == id);

            if (reference == null) return NotFound();

            return View(reference);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reference = await _context.PatientReferences.FindAsync(id);
            if (reference != null)
            {
                _context.PatientReferences.Remove(reference);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", new { patientId = reference?.PatientId });
        }
    }
}
