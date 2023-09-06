using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.EF.App;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
#pragma warning disable CS1591

namespace WebApp.Controllers
{
    public class StylistsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUOW _uow;

        public StylistsController(ApplicationDbContext context, UserManager<AppUser> userManager, IAppUOW uow)
        {
            _context = context;
            _userManager = userManager;
            _uow = uow;
        }

        // GET: Stylists
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.StylistRepository.AllAsync();
            return View(vm);
        }

        // GET: Stylists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stylist = await _uow.StylistRepository.FindAsync(id.Value);
            if (stylist == null)
            {
                return NotFound();
            }

            return View(stylist);
        }

        // GET: Stylists/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address");
            return View();
        }

        // POST: Stylists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Stylist stylist)
        {
            if (ModelState.IsValid)
            {
                _uow.StylistRepository.Add(stylist);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", stylist.AppUserId);
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address", stylist.SalonId);
            return View(stylist);
        }

        // GET: Stylists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stylist = await _uow.StylistRepository.FindAsync(id.Value);
            if (stylist == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", stylist.AppUserId);
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address", stylist.SalonId);
            return View(stylist);
        }

        // POST: Stylists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Stylist stylist)
        {
            if (id != stylist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.StylistRepository.Update(stylist);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", stylist.AppUserId);
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address", stylist.SalonId);
            return View(stylist);
        }

        // GET: Stylists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stylist = await _uow.StylistRepository.FindAsync(id.Value);
            if (stylist == null)
            {
                return NotFound();
            }

            return View(stylist);
        }

        // POST: Stylists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.StylistRepository.RemoveAsync(id);
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
