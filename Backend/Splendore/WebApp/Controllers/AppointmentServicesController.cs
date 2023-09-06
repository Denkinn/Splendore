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
    public class AppointmentServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AppointmentServices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AppointmentServices.Include(a => a.Appointment).Include(a => a.SalonService);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AppointmentServices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.AppointmentServices == null)
            {
                return NotFound();
            }

            var appointmentService = await _context.AppointmentServices
                .Include(a => a.Appointment)
                .Include(a => a.SalonService)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointmentService == null)
            {
                return NotFound();
            }

            return View(appointmentService);
        }

        // GET: AppointmentServices/Create
        public IActionResult Create()
        {
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "Id", "Id");
            ViewData["SalonServiceId"] = new SelectList(_context.SalonServices, "Id", "Id");
            return View();
        }

        // POST: AppointmentServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalonServiceId,AppointmentId,Id")] AppointmentService appointmentService)
        {
            if (ModelState.IsValid)
            {
                appointmentService.Id = Guid.NewGuid();
                _context.Add(appointmentService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "Id", "Id", appointmentService.AppointmentId);
            ViewData["SalonServiceId"] = new SelectList(_context.SalonServices, "Id", "Id", appointmentService.SalonServiceId);
            return View(appointmentService);
        }

        // GET: AppointmentServices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.AppointmentServices == null)
            {
                return NotFound();
            }

            var appointmentService = await _context.AppointmentServices.FindAsync(id);
            if (appointmentService == null)
            {
                return NotFound();
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "Id", "Id", appointmentService.AppointmentId);
            ViewData["SalonServiceId"] = new SelectList(_context.SalonServices, "Id", "Id", appointmentService.SalonServiceId);
            return View(appointmentService);
        }

        // POST: AppointmentServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SalonServiceId,AppointmentId,Id")] AppointmentService appointmentService)
        {
            if (id != appointmentService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointmentService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentServiceExists(appointmentService.Id))
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
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "Id", "Id", appointmentService.AppointmentId);
            ViewData["SalonServiceId"] = new SelectList(_context.SalonServices, "Id", "Id", appointmentService.SalonServiceId);
            return View(appointmentService);
        }

        // GET: AppointmentServices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.AppointmentServices == null)
            {
                return NotFound();
            }

            var appointmentService = await _context.AppointmentServices
                .Include(a => a.Appointment)
                .Include(a => a.SalonService)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointmentService == null)
            {
                return NotFound();
            }

            return View(appointmentService);
        }

        // POST: AppointmentServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.AppointmentServices == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AppointmentServices'  is null.");
            }
            var appointmentService = await _context.AppointmentServices.FindAsync(id);
            if (appointmentService != null)
            {
                _context.AppointmentServices.Remove(appointmentService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentServiceExists(Guid id)
        {
          return (_context.AppointmentServices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
