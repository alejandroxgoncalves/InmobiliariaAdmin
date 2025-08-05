using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inmobiliaria.Dominio;
using Inmobiliaria.Repositorios;

namespace CapaPresentacionAdmin.Controllers
{
    public class PropiedadController : Controller
    {
        private readonly InmobiliariaContext _context;

        public PropiedadController(InmobiliariaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string tipoOperacion, string tipoPropiedad, double? valorMin, double? valorMax)
        {
            ViewBag.TipoOperaciones = new SelectList(Enum.GetNames(typeof(TipoOperacion)));

            var tiposPropiedad = await _context.Propiedades
                .Select(p => p.TipoPropiedad)
                .Distinct()
                .ToListAsync();
            ViewBag.TiposPropiedad = new SelectList(tiposPropiedad);

            var query = _context.Propiedades
                .Include(p => p.Imagenes)
                .AsQueryable();

            if (!string.IsNullOrEmpty(tipoOperacion) && Enum.TryParse<TipoOperacion>(tipoOperacion, out var tipoOperacionEnum))
                query = query.Where(p => p.TipoOperacion == tipoOperacionEnum);

            if (!string.IsNullOrEmpty(tipoPropiedad))
                query = query.Where(p => p.TipoPropiedad == tipoPropiedad);

            if (valorMin.HasValue)
                query = query.Where(p => p.Valor >= valorMin);

            if (valorMax.HasValue)
                query = query.Where(p => p.Valor <= valorMax);

            return View(await query.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var propiedad = await _context.Propiedades
                .Include(p => p.Imagenes)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (propiedad == null) return NotFound();

            return View(propiedad);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Titulo,TipoPropiedad,Descripcion,Direccion,SuperficieTotal,SuperficieConstruida,Valor,Banios,Dormitorios,TieneGarage,Antiguedad,Activa,TipoOperacion")] Propiedad propiedad,
            List<IFormFile> imagenes)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(propiedad);
                    await _context.SaveChangesAsync();

                    if (imagenes != null && imagenes.Any())
                    {
                        var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        var carpetaDestino = Path.Combine(wwwrootPath, "imagenes", propiedad.Id.ToString());
                        Directory.CreateDirectory(carpetaDestino);

                        foreach (var archivo in imagenes)
                        {
                            if (archivo.Length == 0) continue;

                            var nombreArchivo = Path.GetFileName(archivo.FileName);
                            var rutaCompleta = Path.Combine(carpetaDestino, nombreArchivo);

                            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                            {
                                await archivo.CopyToAsync(stream);
                            }

                            var imagen = new Imagen
                            {
                                Url = $"/imagenes/{propiedad.Id}/{nombreArchivo}",
                                PropiedadId = propiedad.Id
                            };

                            _context.Imagenes.Add(imagen);
                        }

                        await _context.SaveChangesAsync();
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al crear propiedad o guardar imagenes: " + ex.Message);
                    ModelState.AddModelError("", "Ocurrió un error al guardar la propiedad o las imágenes.");
                }
            }

            return View(propiedad);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var propiedad = await _context.Propiedades
                .Include(p => p.Imagenes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (propiedad == null) return NotFound();

            return View(propiedad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Titulo,TipoPropiedad,Descripcion,Direccion,SuperficieTotal,SuperficieConstruida,Valor,Banios,Dormitorios,TieneGarage,Antiguedad,Activa,TipoOperacion")] Propiedad propiedad,
            List<IFormFile> imagenes)
        {
            if (id != propiedad.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propiedad);
                    await _context.SaveChangesAsync();

                    if (imagenes != null && imagenes.Any())
                    {
                        var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        var carpetaDestino = Path.Combine(wwwrootPath, "imagenes", propiedad.Id.ToString());
                        Directory.CreateDirectory(carpetaDestino);

                        foreach (var archivo in imagenes)
                        {
                            if (archivo.Length == 0) continue;

                            var nombreArchivo = Path.GetFileName(archivo.FileName);
                            var rutaCompleta = Path.Combine(carpetaDestino, nombreArchivo);

                            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                            {
                                await archivo.CopyToAsync(stream);
                            }

                            var imagen = new Imagen
                            {
                                Url = $"/imagenes/{propiedad.Id}/{nombreArchivo}",
                                PropiedadId = propiedad.Id
                            };

                            _context.Imagenes.Add(imagen);
                        }

                        await _context.SaveChangesAsync();
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al editar propiedad o guardar imagenes: " + ex.Message);
                    ModelState.AddModelError("", "Ocurrió un error al guardar los cambios o las imágenes.");
                }
            }

            propiedad.Imagenes = await _context.Imagenes
                .Where(i => i.PropiedadId == propiedad.Id)
                .ToListAsync();

            return View(propiedad);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var propiedad = await _context.Propiedades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propiedad == null) return NotFound();

            return View(propiedad);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propiedad = await _context.Propiedades.FindAsync(id);
            if (propiedad != null)
            {
                _context.Propiedades.Remove(propiedad);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PropiedadExists(int id)
        {
            return _context.Propiedades.Any(e => e.Id == id);
        }
    }
}
