using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stoiko3.Models;
using Supabase.Postgrest;

namespace stoiko3.Controllers
{
    public class PerfilUsuController : Controller
    {

        private readonly Supabase.Client _supabase;

        public PerfilUsuController(Supabase.Client supabase)
        {
            _supabase = supabase;
        }
    

        // GET: PerfilController/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: PerfilUsu/MiPerfil
        public async Task<IActionResult> MiPerfil()
        {
        int idUsuario = HttpContext.Session.GetInt32("IdUsuario") ?? 0;
        if (idUsuario == 0) return RedirectToAction("Login", "Usuario");

        var response = await _supabase.From<PerfilUsu>()
            .Where(p => p.IdPerfil == idUsuario)
            .Get();

        var perfil = response.Models.FirstOrDefault();
        if (perfil == null) return NotFound();

        return View(perfil);
        }

        // POST: PerfilUsu/MiPerfil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MiPerfil(PerfilUsu perfil)
        {
            int idUsuario = HttpContext.Session.GetInt32("IdUsuario") ?? 0;
            if (idUsuario == 0 || idUsuario != perfil.IdPerfil) return Forbid();

            if (!ModelState.IsValid) return View(perfil);

            await _supabase.From<PerfilUsu>()
                .Where(p => p.IdPerfil == idUsuario)
                .Update(perfil);

            return RedirectToAction(nameof(MiPerfil));
        }
        
        // GET: PerfilUsu/CompletarPerfil
        public IActionResult CompletarPerfil()
        {
            int idUsuario = HttpContext.Session.GetInt32("IdUsuario") ?? 0;
            if (idUsuario == 0) return RedirectToAction("Login", "Usuario");

            return View();
        }

        // POST: PerfilUsu/CompletarPerfil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompletarPerfil(string nomPerfil)
        {
            int idUsuario = HttpContext.Session.GetInt32("IdUsuario") ?? 0;
            int numRachas = HttpContext.Session.GetInt32("NumeroRachas") ?? 0;
            if (idUsuario == 0) return RedirectToAction("Login", "Usuario");

            var nuevoPerfil = new PerfilUsu
            {
                IdPerfil = idUsuario,   // mismo id que usuario.IdUsu (relación 1 a 1)
                NomPerfil = nomPerfil,
                ScoreTotal = 0, 
                NumeroRachas = numRachas
            };

            await _supabase.From<PerfilUsu>().Insert(nuevoPerfil);

            return RedirectToAction("Index", "Home");
        }
    }
   

}
