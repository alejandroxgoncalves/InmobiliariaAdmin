using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inmobiliaria.Dominio;
using Inmobiliaria.Repositorios;

namespace CapaPresentacionAdmin.Controllers
{
    public class AgenteController : Controller
    {
        private readonly InmobiliariaContext _context;

        public AgenteController(InmobiliariaContext context)
        {
            _context = context;
        }

        // GET: Agente
        public async Task<IActionResult> Index()
        {
            return View(await _context.Agentes.ToListAsync());
        }

        // GET: Agente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agente = await _context.Agentes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agente == null)
            {
                return NotFound();
            }

            return View(agente);
        }

        // GET: Agente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Email,Telefono,Direccion,Documento")] Agente agente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agente);
        }

        // GET: Agente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agente = await _context.Agentes.FindAsync(id);
            if (agente == null)
            {
                return NotFound();
            }
            return View(agente);
        }

        // POST: Agente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Email,Telefono,Direccion,Documento")] Agente agente)
        {
            if (id != agente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgenteExists(agente.Id))
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
            return View(agente);
        }

        // GET: Agente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agente = await _context.Agentes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agente == null)
            {
                return NotFound();
            }

            return View(agente);
        }

        // POST: Agente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agente = await _context.Agentes.FindAsync(id);
            if (agente != null)
            {
                _context.Agentes.Remove(agente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgenteExists(int id)
        {
            return _context.Agentes.Any(e => e.Id == id);
        }
    }
}
