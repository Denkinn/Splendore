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
#pragma warning disable CS1591

namespace WebApp.Controllers
{
    public class SalonsController : Controller
    {
        private readonly IAppUOW _uow;

        public SalonsController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Salons
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.SalonRepository.AllAsync();
            return View(vm);
        }

        // GET: Salons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var salon = await _uow.SalonRepository.FindAsync(id.Value);
            
            if (salon == null) return NotFound();

            return View(salon);
        }

        // GET: Salons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Salons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Salon salon)
        {
            if (ModelState.IsValid)
            {
                _uow.SalonRepository.Add(salon);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salon);
        }

        // GET: Salons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var salon = await _uow.SalonRepository.FindAsync(id.Value);
            if (salon == null) return NotFound();
            return View(salon);
        }

        // POST: Salons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Salon salon)
        {
            if (id != salon.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _uow.SalonRepository.Update(salon);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(salon);
        }

        // GET: Salons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var salon = await _uow.SalonRepository.FindAsync(id.Value);
            if (salon == null) return NotFound();

            return View(salon);
        }

        // POST: Salons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.SalonRepository.RemoveAsync(id);
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}