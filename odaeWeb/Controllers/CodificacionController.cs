using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using odaeWeb.Models.DB;
using odaeWeb.Models;
using odaeWeb.Helpers;
using System.Data.Common;
using System.Data.SqlClient;

namespace odaeWeb.Controllers
{
    [Authorize(Roles ="2")]
    public class CodificacionController : Controller
    {
        private readonly odaeDBContext _context;
        private int _codificador;
        private int _fase;

        public CodificacionController(odaeDBContext context)
        {
            _context = context;
        }


        private bool IsUserActive(string userId)
        {
            var usuario = _context.User.FirstOrDefault(u => u.UserId == userId);
            return usuario.UserActivo;
        }

        private bool getFaseCodificador()
        {
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            string userId = identity.GetSpecificClaim(ClaimTypes.Sid);
            if (IsUserActive(userId) && Int32.TryParse(identity.GetSpecificClaim(ClaimTypes.UserData), out _codificador) && Int32.TryParse(identity.GetSpecificClaim(ClaimTypes.Version), out _fase))
            {
                return true;
            }
            else RedirectToAction("Logout", "Account");
            return false;
        }


        // GET: Codificacion
        public async Task<IActionResult> Index(int? page, int filter = -1)
        {
            getFaseCodificador();
            var imagenesCodificador = _context.Codificacion.Where(c => c.CodificadorId == _codificador && c.FaseId == _fase).OrderBy(o => o.RowIndex).Include(m => m.Material);
            if (filter == 2)
            {
                imagenesCodificador = _context.Codificacion.Where(c => c.CodificadorId == _codificador && c.FaseId == _fase && c.Estado > 0 && c.Estado < 4).OrderBy(o => o.RowIndex).Include(m => m.Material);
            }
            else if (filter != -1)
            {
                imagenesCodificador = _context.Codificacion.Where(c => c.CodificadorId == _codificador && c.FaseId == _fase && c.Estado == filter).OrderBy(o => o.RowIndex).Include(m => m.Material);
            }
            //var estados = imagenesCodificador.GroupBy(p => p.Estado).Select(g => new { name = g.Key, count = g.Count() }).ToListAsync();
            //var count0 = await imagenesCodificador.Where(o => o.Estado == 0).CountAsync();
            //var count1 = await imagenesCodificador.Where(o => o.Estado == 1 || o.Estado == 2 || o.Estado == 3).CountAsync();
            //var count2 = await imagenesCodificador.Where(o => o.Estado == 4).CountAsync();
            //var total = count0 + count1 + count2;

            ViewData["Filtros"] = new SelectList(new[]
                {
                    new { valor = "-1", texto = "Mostrar todo"},
                    new { valor = "0", texto = "Sin codificar" },
                    new { valor = "2", texto = "Incompleto" },
                    new { valor = "4", texto = "Finalizado" }
                },
                "valor", "texto");

            //_listaImagenes = await imagenesCodificador.ToArrayAsync();

            int pageSize = 24;
            return View(await PaginatedList<Codificacion>.CreateAsync(imagenesCodificador.AsNoTracking(), page ?? 1, pageSize, filter));

            //return View(_listaImagenes);
        }



        // GET: Codificacion/Edit/5
        public async Task<IActionResult> Edit(int materialId, int filter = -1)
        {
            getFaseCodificador();
            
            //var codificacion = await _context.Codificacion.SingleOrDefaultAsync(m => m.CodificadorId == codificadorId && m.FaseId == faseId && m.MaterialId == materialId);
            var codificacion = await _context.Codificacion.Where(c => c.CodificadorId == _codificador && c.FaseId == _fase && c.MaterialId == materialId).Include(m => m.Material).SingleOrDefaultAsync();

            if (codificacion == null)
            {
                return NotFound();
            }

            CodificacionViewModel model = new CodificacionViewModel(codificacion, filter);

            Navigation item = await GetItem(_codificador, _fase, materialId, filter);

            model.NextItem = item.Next;
            model.PrevItem = item.Previous;
            model.Total = item.Total;
            model.Pos = item.Pos;

            await FillCombos(model);

            return View(model);
        }


        // POST: Codificacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("CodificadorId,FaseId,MaterialId,FileName,TieneDuplicado,NivelId,NivelComentario,CursoId,EjeId,ObjetivoId,ObjetivoComentario,HabilidadId,HabilidadComentario,TipoTareaId,TipoTareaComentario,CorreccionProfesor,ErrorEjecucion,TrabajaDinero,Observaciones,Filtro")] CodificacionViewModel codificacion)
        {
            if (codificacion == null)
            {
                return NotFound();
            }

            codificacion.UpdateEstado();

            if (ModelState.IsValid)
            {
                try
                {
                    Codificacion cd = codificacion.getCodificacion();
                    _context.Update(cd);
                    _context.Entry(cd).State = EntityState.Modified;
                    _context.Entry(cd).Property(o => o.RowIndex).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CodificacionExists(codificacion.CodificadorId, codificacion.FaseId, codificacion.MaterialId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit), new { @materialId = codificacion.MaterialId, @filter = codificacion.Filtro });
                
            }

            await FillCombos(codificacion);

            return View(codificacion);
        }

        public JsonResult getObjetivos(int cursoId, int ejeId) // its a GET, not a POST
        {
            var objetivos = _context.Objetivo.Where(o => o.CursoId == cursoId && o.EjeId == ejeId).ToList();
            return Json(new SelectList(objetivos, "ObjetivoId", "ObjetivoAprendizaje"));
        }


        private bool CodificacionExists(int? codificadorId, int? faseId, int? materialId)
        {
            return _context.Codificacion.Any(e => e.CodificadorId == codificadorId && e.FaseId == faseId && e.MaterialId == materialId);
        }


        private async Task FillCombos(CodificacionViewModel codificacion)
        {
            ViewData["NivelId"] = new SelectList(_context.Nivel, "NivelId", "NivelDemandaCognitiva", codificacion.NivelId);
            ViewData["CursoId"] = new SelectList(_context.Curso, "CursoId", "NombreCurso", codificacion.CursoId);
            ViewData["EjeId"] = new SelectList(_context.Eje, "EjeId", "DescripcionEje", codificacion.EjeId);

            bool cursoEje = codificacion.CursoId != null && codificacion.EjeId != null;

            if (cursoEje)
            {
                var objetivos = await _context.Objetivo.Where(o => o.CursoId == codificacion.CursoId && o.EjeId == codificacion.EjeId).ToListAsync();
                ViewData["ObjetivoId"] = new SelectList(objetivos, "ObjetivoId", "ObjetivoAprendizaje", codificacion.ObjetivoId);
            }

            ViewData["HabilidadId"] = new SelectList(_context.Habilidad, "HabilidadId", "NombreHabilidad", codificacion.HabilidadId);
            ViewData["TipoTareaId"] = new SelectList(_context.TipoTarea, "TipoTareaId", "NombreTipoTarea", codificacion.TipoTareaId);

            var SelSiNo = new[]
                {
                    new { valor = "", texto = "Seleccionar..." },
                    new { valor = "True", texto = "Sí" },
                    new { valor = "False", texto = "No" }
                };

            var SiNo = new[]
                {
                    new { valor = "True", texto = "Sí" },
                    new { valor = "False", texto = "No" }
                };

            var Tmp = (codificacion.CorreccionProfesor == null) ? SelSiNo : SiNo;
            ViewData["CorreccionProfesor"] = new SelectList(Tmp, "valor", "texto", codificacion.CorreccionProfesor);

            Tmp = (codificacion.ErrorEjecucion == null) ? SelSiNo : SiNo;
            ViewData["ErrorEjecucion"] = new SelectList(Tmp, "valor", "texto", codificacion.ErrorEjecucion);

            Tmp = (codificacion.TrabajaDinero == null) ? SelSiNo : SiNo;
            ViewData["TrabajaDinero"] = new SelectList(Tmp, "valor", "texto", codificacion.TrabajaDinero);

            if ((bool)codificacion.TieneDuplicado)
            {
                var duplicados = await _context.Material.Where(d => d.Original == codificacion.MaterialId).ToListAsync();
                ViewData["Duplicados"] = new SelectList(duplicados, "MaterialId", "FileName");
            }
        }



        private async Task<Navigation> GetItem(int codId, int faseId, int materialId, int filter)
        {
            string sqlEstado;

            if (filter == -1)
            {
                sqlEstado = ")";
            }
            else if (filter > 0 && filter < 4)
            {
                sqlEstado = " AND Estado BETWEEN 1 AND 3)";
            }
            else
            {
                sqlEstado = " AND Estado = " + filter.ToString() + ")";
            }

            string c = codId.ToString();
            string f = faseId.ToString();
            string m = materialId.ToString();

            string sql0 = "SELECT COUNT(MaterialId) FROM codificacion WHERE (CodificadorId = " + c + " AND FaseId = " + f + sqlEstado;
            string sql1 = "SELECT Fila FROM (SELECT ROW_NUMBER() OVER (ORDER BY (RowIndex)) AS Fila, MaterialId FROM codificacion WHERE CodificadorId=" + c + " AND FaseId=" + f + sqlEstado + " B WHERE MaterialId=" + m;
            string sql2 = "SELECT MaterialId FROM (SELECT ROW_NUMBER() OVER (ORDER BY (RowIndex)) AS Fila, MaterialId FROM codificacion WHERE CodificadorId = " + c + " AND FaseId = " + f + sqlEstado + " A WHERE Fila = ";

            Navigation item = new Navigation();

            var conn = _context.Database.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {

                    DbDataReader reader;

                    command.CommandText = sql0;
                    reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            item.Total = reader.GetInt32(0);
                        }
                    }
                    else item.Total = 0;
                    reader.Dispose();

                    command.CommandText = sql1;
                    reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            item.Pos = unchecked((int)reader.GetInt64(0));
                        }
                    }
                    else item.Pos = 0;
                    reader.Dispose();

                    command.CommandText = sql2 + (item.Pos - 1).ToString();
                    reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            item.Previous = reader.GetInt32(0);
                        }
                    }
                    reader.Dispose();

                    command.CommandText = sql2 + (item.Pos + 1).ToString();
                    reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            item.Next = reader.GetInt32(0);
                        }
                    }
                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return item;
        }


    }
}
