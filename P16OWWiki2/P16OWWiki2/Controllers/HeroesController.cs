using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using P16OWWiki2.Data;
using P16OWWiki2.Models;
using P16OWWiki2.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace P16OWWiki2.Controllers
{
    public class HeroesController : Controller
    {
        private readonly HeroeContext _context;
        private readonly IWebHostEnvironment _environment;

        public HeroesController(HeroeContext context, IWebHostEnvironment enviroment)
        {
            _context = context;
            _environment = enviroment;
        }

        // GET: Heroes
        public async Task<IActionResult> Index(string ordenar, string bnom, string brol, string bnac, string bafi)
        {
            var item = from lista in _context.HeroeSet
                       select lista;
            IQueryable<string> rolQuery = from lista in _context.HeroeSet
                                                    orderby lista.Rol
                                                    select lista.Rol;
            IQueryable<string> nacQuery = from lista in _context.HeroeSet
                                                    orderby lista.Nacionalidad
                                                    select lista.Nacionalidad;
            IQueryable<string> afiQuery = from listo in _context.HeroeSet
                                                    orderby listo.Afiliacion1
                                                    select listo.Afiliacion1;

            ViewData["RolSort"] = String.IsNullOrEmpty(ordenar) || ordenar == "RolAs" ? "RolDes" : "RolAs";
            switch(ordenar)
            {
                case "RolDes":
                    item = item.OrderByDescending(r => r.Rol);
                    break;
                case "RolAs":
                    item = item.OrderBy(r => r.Rol);
                    break;
                default:
                    break;
            }

            if (!String.IsNullOrEmpty(bnom))
            {
                item = item.Where(s => s.Nombre.Contains(bnom));
            }
            if(!String.IsNullOrEmpty(brol))
            {
                item = item.Where(rol => rol.Rol == brol);
            }
            if (!String.IsNullOrEmpty(bnac))
            {
                item = item.Where(na => na.Nacionalidad == bnac);
            }

            if (!String.IsNullOrEmpty(bafi))
            {
                item = item.Where(af => af.Afiliacion1 == bafi);
            }

            var res = new BuscaVM
            {
                LosHeroes = await item.ToListAsync(),
                LosRoles = new SelectList(await rolQuery.Distinct().ToListAsync()),
                LasNacionalidades = new SelectList(await nacQuery.Distinct().ToListAsync()),
                LasAfiliaciones = new SelectList(await afiQuery.Distinct().ToListAsync()),
            };

            return View(res);
        }

        // GET: Heroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heroe = await _context.HeroeSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (heroe == null)
            {
                return NotFound();
            }

            return View(heroe);
        }

        // GET: Heroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Heroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HeroeVM heroe)
        {
            if (ModelState.IsValid)
            {
                string UniqueFileName = UploadedFile(heroe);
                Heroe elemento = new Heroe
                {
                    Nombre = heroe.Nombre,
                    RNombre = heroe.RNombre,
                    Edad = heroe.Edad,
                    Ocupa = heroe.Ocupa,
                    Nacionalidad = heroe.Nacionalidad,
                    Afiliacion1 = heroe.Afiliacion1,
                    Afiliacion2 = heroe.Afiliacion2,
                    Rol = heroe.Rol,
                    Salud = heroe.Salud,
                    Nomfoto = UniqueFileName,
                };

                _context.Add(elemento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(heroe);
        }

        private string UploadedFile(HeroeVM heroe)
        {
            string uniqueFileName = null;
            if (heroe.Foto != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "Fotos");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + heroe.Foto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    heroe.Foto.CopyTo(fileStream);
                }
            }
            else
            {
                // Si no se subió imagen, asigna una imagen por defecto
                uniqueFileName = "default.png";
            }

            return uniqueFileName;
        }

        // GET: Heroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heroe = await _context.HeroeSet.FindAsync(id);
            if (heroe == null)
            {
                return NotFound();
            }
            return View(heroe);
        }

        // POST: Heroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Heroe heroe)
        {
            if (id != heroe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(heroe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeroeExists(heroe.Id))
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
            return View(heroe);
        }

        // GET: Heroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heroe = await _context.HeroeSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (heroe == null)
            {
                return NotFound();
            }

            return View(heroe);
        }

        // POST: Heroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var heroe = await _context.HeroeSet.FindAsync(id);
            if (heroe != null)
            {
                _context.HeroeSet.Remove(heroe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeroeExists(int id)
        {
            return _context.HeroeSet.Any(e => e.Id == id);
        }
    }
}
