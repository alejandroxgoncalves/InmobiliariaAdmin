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
    public class VisitaController : Controller
    {
        private readonly InmobiliariaContext _context;

        public VisitaController(InmobiliariaContext context)
        {
            _context = context;
        }

        // GET: Visita
        public async Task<IActionResult> Index()
        {
            var inmobiliariaContext = _context.Visitas.Include(v => v.Agente).Include(v => v.Cliente).Include(v => v.Propiedad);
            return View(await inmobiliariaContext.ToListAsync());
        }

        // GET: Visita/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visita = await _context.Visitas
                .Include(v => v.Agente)
                .Include(v => v.Cliente)
                .Include(v => v.Propiedad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visita == null)
            {
                return NotFound();
            }

            return View(visita);
        }

        // GET: Visita/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
            ViewData["AgenteId"] = new SelectList(_context.Agentes, "Id", "Nombre");
            ViewData["PropiedadId"] = new SelectList(_context.Propiedades, "Id", "Titulo");

            return View();
        }

        // POST: Visita/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Estado,Observaciones,PropiedadId,ClienteId,AgenteId")] Visita visita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
            ViewData["AgenteId"] = new SelectList(_context.Agentes, "Id", "Nombre");
            ViewData["PropiedadId"] = new SelectList(_context.Propiedades, "Id", "Titulo");

            return View(visita);
        }

        // GET: Visita/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visita = await _context.Visitas.FindAsync(id);
            if (visita == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
            ViewData["AgenteId"] = new SelectList(_context.Agentes, "Id", "Nombre");
            ViewData["PropiedadId"] = new SelectList(_context.Propiedades, "Id", "Titulo");

            return View(visita);
        }

        // POST: Visita/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Estado,Observaciones,PropiedadId,ClienteId,AgenteId")] Visita visita)
        {
            if (id != visita.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitaExists(visita.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
            ViewData["AgenteId"] = new SelectList(_context.Agentes, "Id", "Nombre");
            ViewData["PropiedadId"] = new SelectList(_context.Propiedades, "Id", "Titulo");

            return View(visita);
        }

        // GET: Visita/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visita = await _context.Visitas
                .Include(v => v.Agente)
                .Include(v => v.Cliente)
                .Include(v => v.Propiedad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visita == null)
            {
                return NotFound();
            }

            return View(visita);
        }

        // POST: Visita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visita = await _context.Visitas.FindAsync(id);
            if (visita != null)
            {
                _context.Visitas.Remove(visita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitaExists(int id)
        {
            return _context.Visitas.Any(e => e.Id == id);
        }
    }
}
