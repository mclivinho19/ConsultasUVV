using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ConsultasUVV.Data;
using ConsultasUVV.Models;

namespace ConsultasUVV.Controllers
{
    [Authorize] // Protege todas as actions deste controller
    public class ConsultasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsultasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Consultas
        public async Task<IActionResult> Index()
        {
            int usuarioId = GetUsuarioId();
            var consultas = await _context.Consultas
                .Where(c => c.UsuarioId == usuarioId)
                .OrderBy(c => c.DataHora)
                .ToListAsync();
            return View(consultas);
        }

        // GET: /Consultas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Consultas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Consulta consulta)
        {
            consulta.UsuarioId = GetUsuarioId();
            if (ModelState.IsValid)
            {
                _context.Add(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consulta);
        }

        // GET: /Consultas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == GetUsuarioId());
            if (consulta == null) return NotFound();

            return View(consulta);
        }

        // POST: /Consultas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Consulta consulta)
        {
            if (id != consulta.Id) return NotFound();

            consulta.UsuarioId = GetUsuarioId();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consulta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultaExists(consulta.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(consulta);
        }

        // GET: /Consultas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == GetUsuarioId());
            if (consulta == null) return NotFound();

            return View(consulta);
        }

        // POST: /Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == GetUsuarioId());
            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private int GetUsuarioId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        private bool ConsultaExists(int id)
        {
            return _context.Consultas.Any(e => e.Id == id);
        }
    }
}