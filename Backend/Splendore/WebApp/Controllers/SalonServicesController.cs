using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.EF.App;
using Domain.App;
#pragma warning disable CS1591

namespace WebApp.Controllers
{
    public class SalonServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalonServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SalonServices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SalonServices.Include(s => s.Salon).Include(s => s.Service);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SalonServices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.SalonServices == null)
            {
                return NotFound();
            }

            var salonService = await _context.SalonServices
                .Include(s => s.Salon)
                .Include(s => s.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salonService == null)
            {
                return NotFound();
            }

            return View(salonService);
        }

        // GET: SalonServices/Create
        public IActionResult Create()
        {
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Name");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");
            return View();
        }

        // POST: SalonServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Price,Time,SalonId,ServiceId,Id")] SalonService salonService)
        {
            if (ModelState.IsValid)
            {
                salonService.Id = Guid.NewGuid();
                _context.Add(salonService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address", salonService.SalonId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", salonService.ServiceId);
            return View(salonService);
        }

        // GET: SalonServices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.SalonServices == null)
            {
                return NotFound();
            }

            var salonService = await _context.SalonServices.FindAsync(id);
            if (salonService == null)
            {
                return NotFound();
            }
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address", salonService.SalonId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", salonService.ServiceId);
            return View(salonService);
        }

        // POST: SalonServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Price,Time,SalonId,ServiceId,Id")] SalonService salonService)
        {
            if (id != salonService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salonService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalonServiceExists(salonService.Id))
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
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address", salonService.SalonId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", salonService.ServiceId);
            return View(salonService);
        }

        // GET: SalonServices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.SalonServices == null)
            {
                return NotFound();
            }

            var salonService = await _context.SalonServices
                .Include(s => s.Salon)
                .Include(s => s.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salonService == null)
            {
                return NotFound();
            }

            return View(salonService);
        }

        // POST: SalonServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.SalonServices == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SalonServices'  is null.");
            }
            var salonService = await _context.SalonServices.FindAsync(id);
            if (salonService != null)
            {
                _context.SalonServices.Remove(salonService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalonServiceExists(Guid id)
        {
          return (_context.SalonServices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
