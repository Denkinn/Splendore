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
    public class AppointmentStatusesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentStatusesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AppointmentStatuses
        public async Task<IActionResult> Index()
        {
              return _context.AppointmentStatuses != null ? 
                          View(await _context.AppointmentStatuses.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AppointmentStatuses'  is null.");
        }

        // GET: AppointmentStatuses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.AppointmentStatuses == null)
            {
                return NotFound();
            }

            var appointmentStatus = await _context.AppointmentStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointmentStatus == null)
            {
                return NotFound();
            }

            return View(appointmentStatus);
        }

        // GET: AppointmentStatuses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppointmentStatuses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] AppointmentStatus appointmentStatus)
        {
            if (ModelState.IsValid)
            {
                appointmentStatus.Id = Guid.NewGuid();
                _context.Add(appointmentStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointmentStatus);
        }

        // GET: AppointmentStatuses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.AppointmentStatuses == null)
            {
                return NotFound();
            }

            var appointmentStatus = await _context.AppointmentStatuses.FindAsync(id);
            if (appointmentStatus == null)
            {
                return NotFound();
            }
            return View(appointmentStatus);
        }

        // POST: AppointmentStatuses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] AppointmentStatus appointmentStatus)
        {
            if (id != appointmentStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointmentStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentStatusExists(appointmentStatus.Id))
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
            return View(appointmentStatus);
        }

        // GET: AppointmentStatuses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.AppointmentStatuses == null)
            {
                return NotFound();
            }

            var appointmentStatus = await _context.AppointmentStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointmentStatus == null)
            {
                return NotFound();
            }

            return View(appointmentStatus);
        }

        // POST: AppointmentStatuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.AppointmentStatuses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AppointmentStatuses'  is null.");
            }
            var appointmentStatus = await _context.AppointmentStatuses.FindAsync(id);
            if (appointmentStatus != null)
            {
                _context.AppointmentStatuses.Remove(appointmentStatus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentStatusExists(Guid id)
        {
          return (_context.AppointmentStatuses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
