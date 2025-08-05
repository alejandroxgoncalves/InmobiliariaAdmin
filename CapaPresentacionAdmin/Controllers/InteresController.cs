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
    public class InteresController : Controller
    {
        private readonly InmobiliariaContext _context;

        public InteresController(InmobiliariaContext context)
        {
            _context = context;
        }

        // GET: Interes
        public async Task<IActionResult> Index()
        {
            var inmobiliariaContext = _context.Intereses.Include(i => i.Cliente);
            return View(await inmobiliariaContext.ToListAsync());
        }

        // GET: Interes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var interes = await _context.Intereses
                .Include(i => i.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (interes == null)
                return NotFound();

            return View(interes);
        }

        // GET: Interes/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");

            var tiposPropiedad = _context.Propiedades
                .Select(p => p.TipoPropiedad)
                .Distinct()
                .ToList();
            ViewBag.TiposPropiedad = new SelectList(tiposPropiedad);

            return View();
        }

        // POST: Interes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,TipoPropiedad,ValorMaximo,Observaciones,ClienteId")] Interes interes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(interes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre", interes.ClienteId);

            var tiposPropiedad = _context.Propiedades
                .Select(p => p.TipoPropiedad)
                .Distinct()
                .ToList();
            ViewBag.TiposPropiedad = new SelectList(tiposPropiedad, interes.TipoPropiedad);

            return View(interes);
        }

        // GET: Interes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var interes = await _context.Intereses.FindAsync(id);
            if (interes == null) return NotFound();

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre", interes.ClienteId);

            var tiposPropiedad = _context.Propiedades
                .Select(p => p.TipoPropiedad)
                .Distinct()
                .ToList();
            ViewBag.TiposPropiedad = new SelectList(tiposPropiedad, interes.TipoPropiedad);

            return View(interes);
        }

        // POST: Interes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,TipoPropiedad,ValorMaximo,Observaciones,ClienteId")] Interes interes)
        {
            if (id != interes.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InteresExists(interes.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre", interes.ClienteId);

            var tiposPropiedad = _context.Propiedades
                .Select(p => p.TipoPropiedad)
                .Distinct()
                .ToList();
            ViewBag.TiposPropiedad = new SelectList(tiposPropiedad, interes.TipoPropiedad);

            return View(interes);
        }


        // GET: Interes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var interes = await _context.Intereses
                .Include(i => i.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interes == null)
                return NotFound();

            return View(interes);
        }

        // POST: Interes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interes = await _context.Intereses.FindAsync(id);
            if (interes != null)
            {
                _context.Intereses.Remove(interes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InteresExists(int id)
        {
            return _context.Intereses.Any(e => e.Id == id);
        }
    }

}
