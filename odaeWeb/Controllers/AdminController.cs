using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using odaeWeb.Models.DB;
using odaeWeb.Helpers;

namespace odaeWeb.Controllers
{
    [Authorize(Roles = "0")]
    public class AdminController : Controller
    {
        private readonly odaeDBContext _context;

        public AdminController(odaeDBContext context)
        {
            _context = context;
        }

        // GET: UserFaseCodificadors
        public async Task<IActionResult> Index()
        {
            var odaeDBContext = _context.UserFaseCodificador.Include(u => u.Codificador).Include(u => u.Fase).Include(u => u.User);
            return View(await odaeDBContext.ToListAsync());
        }

        // GET: UserFaseCodificadors/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFaseCodificador = await _context.UserFaseCodificador
                .Include(u => u.Codificador)
                .Include(u => u.Fase)
                .Include(u => u.User)
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (userFaseCodificador == null)
            {
                return NotFound();
            }

            return View(userFaseCodificador);
        }

        // GET: UserFaseCodificadors/Create
        public IActionResult Create()
        {
            ViewData["CodificadorId"] = new SelectList(_context.Codificador, "CodificadorId", "CodificadorId");
            ViewData["FaseId"] = new SelectList(_context.Fase, "FaseId", "NombreFase");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId");
            return View();
        }

        // POST: UserFaseCodificadors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,CodificadorId,FaseId")] UserFaseCodificador userFaseCodificador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userFaseCodificador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodificadorId"] = new SelectList(_context.Codificador, "CodificadorId", "CodificadorId", userFaseCodificador.CodificadorId);
            ViewData["FaseId"] = new SelectList(_context.Fase, "FaseId", "NombreFase", userFaseCodificador.FaseId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", userFaseCodificador.UserId);
            return View(userFaseCodificador);
        }

        // GET: UserFaseCodificadors/Edit/5
        public async Task<IActionResult> Edit(string userId, int faseId, int codId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            
            var userFaseCodificador = await _context.UserFaseCodificador.Include(c => c.Codificador).Include(u => u.User).SingleOrDefaultAsync(m => m.UserId == userId);
            if (userFaseCodificador == null)
            {
                return NotFound();
            }
            ViewData["FaseId"] = new SelectList(_context.Fase, "FaseId", "NombreFase", userFaseCodificador.FaseId);
            return View(userFaseCodificador);
        }


        public JsonResult getToken() // its a GET, not a POST
        {
            return Json(Tools.getToken());
        }

        // POST: UserFaseCodificadors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string nombreCodificador, string email, string token, [Bind("UserId,CodificadorId,FaseId")] UserFaseCodificador userFaseCodificador)
        {
            if (userFaseCodificador.UserId == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userFaseCodificador);

                    var usuario = new User() { UserId = userFaseCodificador.UserId, Token = token, TokenExpiration = DateTime.Now.AddDays(2) };
                    _context.Attach(usuario);
                    _context.Entry(usuario).Property(x => x.Token).IsModified = true;
                    _context.Entry(usuario).Property(x => x.TokenExpiration).IsModified = true;

                    var codificador = new Codificador() { CodificadorId = userFaseCodificador.CodificadorId, NombreCodificador = nombreCodificador, Email = email };
                    _context.Attach(codificador);
                    _context.Entry(codificador).Property(x => x.NombreCodificador).IsModified = true;
                    _context.Entry(codificador).Property(x => x.Email).IsModified = true;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserFaseCodificadorExists(userFaseCodificador.UserId) || !UserExists(userFaseCodificador.UserId) || !CodificadorExists(userFaseCodificador.CodificadorId))
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
            ViewData["FaseId"] = new SelectList(_context.Fase, "FaseId", "NombreFase", userFaseCodificador.FaseId);
            return View(userFaseCodificador);
        }

        // GET: UserFaseCodificadors/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFaseCodificador = await _context.UserFaseCodificador
                .Include(u => u.Codificador)
                .Include(u => u.Fase)
                .Include(u => u.User)
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (userFaseCodificador == null)
            {
                return NotFound();
            }

            return View(userFaseCodificador);
        }

        // POST: UserFaseCodificadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userFaseCodificador = await _context.UserFaseCodificador.SingleOrDefaultAsync(m => m.UserId == id);
            _context.UserFaseCodificador.Remove(userFaseCodificador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserFaseCodificadorExists(string id)
        {
            return _context.UserFaseCodificador.Any(e => e.UserId == id);
        }
        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.UserId == id);
        }
        private bool CodificadorExists(int id)
        {
            return _context.Codificador.Any(e => e.CodificadorId == id);
        }

    }
}
