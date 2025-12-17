using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp_Patient.Data;
using WebApp_Patient.Models;

namespace Patient_WebApp.Controllers
{
    public class AddressesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddressesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var addresses = await _context.Addresses.AsNoTracking().ToListAsync();
            return View(addresses);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var address = await _context.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.AddressId == id);

            if (address == null) return NotFound();

            return View(address);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Address address)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return View(address);
            }

            _context.Add(address);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Address created successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var address = await _context.Addresses.FindAsync(id);
            if (address == null) return NotFound();

            return View(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Address address)
        {
            if (id != address.AddressId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Address updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.AddressId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var address = await _context.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.AddressId == id);

            if (address == null) return NotFound();

            return View(address);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null) return NotFound();

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Address deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.AddressId == id);
        }
    }
}
