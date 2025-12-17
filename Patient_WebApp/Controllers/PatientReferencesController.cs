using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PatientReferences.Include(p => p.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PatientReferences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientReference = await _context.PatientReferences
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.ReferenceId == id);
            if (patientReference == null)
            {
                return NotFound();
            }

            return View(patientReference);
        }

        // GET: PatientReferences/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "FamilyName");
            return View();
        }

        // POST: PatientReferences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReferenceId,PatientId,ReferenceName,Telephone,RelationShip,Address,Religion,Nationality,NationalNo")] PatientReference patientReference)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientReference);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "FamilyName", patientReference.PatientId);
            return View(patientReference);
        }

        // GET: PatientReferences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientReference = await _context.PatientReferences.FindAsync(id);
            if (patientReference == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "FamilyName", patientReference.PatientId);
            return View(patientReference);
        }

        // POST: PatientReferences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReferenceId,PatientId,ReferenceName,Telephone,RelationShip,Address,Religion,Nationality,NationalNo")] PatientReference patientReference)
        {
            if (id != patientReference.ReferenceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientReference);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientReferenceExists(patientReference.ReferenceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "FamilyName", patientReference.PatientId);
            return View(patientReference);
        }

        // GET: PatientReferences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientReference = await _context.PatientReferences
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.ReferenceId == id);
            if (patientReference == null)
            {
                return NotFound();
            }

            return View(patientReference);
        }

        // POST: PatientReferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patientReference = await _context.PatientReferences.FindAsync(id);
            if (patientReference != null)
            {
                _context.PatientReferences.Remove(patientReference);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientReferenceExists(int id)
        {
            return _context.PatientReferences.Any(e => e.ReferenceId == id);
        }
    }
}
