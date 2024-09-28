using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaSistema.Models;

namespace ClinicaSistema.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly dbclinicaContext _context;

        public ConsultasController(dbclinicaContext context)
        {
            _context = context;
        }

        // GET: Consultas
        public async Task<IActionResult> Index()
        {
            var dbclinicaContext = _context.Consultas.Include(c => c.Especialidade).Include(c => c.Medico).Include(c => c.Paciente);
            return View(await dbclinicaContext.ToListAsync());
        }

        // GET: Consultas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Consultas == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas
                .Include(c => c.Especialidade)
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.ConsultaId == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // GET: Consultas/GetMedicosByEspecialidade/5
        public async Task<IActionResult> GetMedicosByEspecialidade(int especialidadeId)
        {
            var medicos = await _context.Medicos
                .Where(m => m.EspecialidadeId == especialidadeId)
                .Select(m => new { m.MedicosId, m.Nome })
                .ToListAsync();

            return Json(medicos);
        }

        // GET: Consultas/Create
        public IActionResult Create()
        {
            // Preenche o ViewBag com as especialidades usando o nome para exibição e o ID como valor
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "EspecialidadeId", "Nome");

            // Não preenche o ViewData["MedicoId"] inicialmente, será preenchido via Ajax

            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nome");

            return View();
        }



        // POST: Consultas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsultaId,EspecialidadeId,MedicoId,PacienteId,Procedimento,DataConsulta")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "EspecialidadeId", "EspecialidadeId", consulta.EspecialidadeId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicosId", "MedicosId", consulta.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "PacienteId", consulta.PacienteId);
            return View(consulta);
        }

        // GET: Consultas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Consultas == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "EspecialidadeId", "Nome", consulta.EspecialidadeId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicosId", "MedicosId", consulta.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nome", consulta.PacienteId);
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsultaId,EspecialidadeId,MedicoId,PacienteId,Procedimento,DataConsulta")] Consulta consulta)
        {
            if (id != consulta.ConsultaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consulta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultaExists(consulta.ConsultaId))
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
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "EspecialidadeId", "EspecialidadeId", consulta.EspecialidadeId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicosId", "MedicosId", consulta.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "PacienteId", consulta.PacienteId);
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Consultas == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas
                .Include(c => c.Especialidade)
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.ConsultaId == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Consultas == null)
            {
                return Problem("Entity set 'dbclinicaContext.Consultas'  is null.");
            }
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultaExists(int id)
        {
          return (_context.Consultas?.Any(e => e.ConsultaId == id)).GetValueOrDefault();
        }
    }
}
