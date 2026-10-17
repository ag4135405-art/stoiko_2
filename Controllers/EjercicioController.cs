using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stoiko3.Models;

namespace stoiko3.Controllers
{
    public class EjercicioController : Controller
    {
        private readonly Supabase.Client _supabase;

        public EjercicioController(Supabase.Client supabase)
        {
            _supabase = supabase;
        }


        // GET: EjercicioController
        public async Task<IActionResult> Index()
        {
            var response = await _supabase.From<Ejercicio>().Get();
            var ejercicios = response.Models;
            return View(ejercicios);
        }

        // GET: EjercicioController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _supabase.From<Ejercicio>()
                .Where(d => d.IdEjercicio == id)
                .Get();

            var ejercicios = response.Models.FirstOrDefault();
            if (ejercicios == null) return NotFound();

            return View(ejercicios);
        }

        // GET: EjercicioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EjercicioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ejercicio ejercicio)
        {
            if (!ModelState.IsValid) return View(ejercicio);

            await _supabase.From<Ejercicio>().Insert(ejercicio);
            return RedirectToAction(nameof(Index));
        }

        // GET: EjercicioController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _supabase.From<Ejercicio>()
                .Where(d => d.IdEjercicio == id)
                .Get();

            var ejercicios = response.Models.FirstOrDefault();
            if (ejercicios == null) return NotFound();

            return View(ejercicios);
        }

        // POST: EjercicioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ejercicio ejercicio)
        {
            if (id != ejercicio.IdEjercicio) return BadRequest();
            if (!ModelState.IsValid) return View(ejercicio);

            await _supabase.From<Ejercicio>()
                .Where(d => d.IdEjercicio == id)
                .Update(ejercicio);

            return RedirectToAction(nameof(Index));
        }

        // GET: EjercicioController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _supabase.From<Ejercicio>()
                .Where(d => d.IdEjercicio == id)
                .Get();

            var ejercicios = response.Models.FirstOrDefault();
            if (ejercicios == null) return NotFound();

            return View(ejercicios);
        }

        // POST: EjercicioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Ejercicio ejercicio)
        {
            await _supabase.From<Ejercicio>()
                .Where(d => d.IdEjercicio == id)
                .Delete();

            return RedirectToAction(nameof(Index));
        }
    }
}
