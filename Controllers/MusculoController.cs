using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stoiko3.Models;

namespace stoiko3.Controllers
{
    public class MusculoController : Controller
    {
        private readonly Supabase.Client _supabase;

        public MusculoController(Supabase.Client supabase)
        {
            _supabase = supabase;
        }


        // GET: MusculoController
        public async Task<IActionResult> Index()
        {
            var response = await _supabase.From<Musculo>().Get();
            var musculos = response.Models;
            return View(musculos);
        }

        // GET: MusculoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _supabase.From<Musculo>()
                .Where(d => d.IdMusculo == id)
                .Get();

            var musculos = response.Models.FirstOrDefault();
            if (musculos == null) return NotFound();

            return View(musculos);
        }

        // GET: MusculoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MusculoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Musculo musculo)
        {
            if (!ModelState.IsValid) return View(musculo);

            await _supabase.From<Musculo>().Insert(musculo);
            return RedirectToAction(nameof(Index));
        }

        // GET: MusculoController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _supabase.From<Musculo>()
                .Where(d => d.IdMusculo == id)
                .Get();

            var musculos = response.Models.FirstOrDefault();
            if (musculos == null) return NotFound();

            return View(musculos);
        }

        // POST: MusculoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Musculo musculo)
        {
            if (id != musculo.IdMusculo) return BadRequest();
            if (!ModelState.IsValid) return View(musculo);

            await _supabase.From<Musculo>()
                .Where(d => d.IdMusculo == id)
                .Update(musculo);

            return RedirectToAction(nameof(Index));
        }

        // GET: MusculoController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _supabase.From<Musculo>()
                .Where(d => d.IdMusculo == id)
                .Get();

            var musculos = response.Models.FirstOrDefault();
            if (musculos == null) return NotFound();

            return View(musculos);
        }

        // POST: MusculoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Musculo musculo)
        {
            await _supabase.From<Musculo>()
                .Where(d => d.IdMusculo == id)
                .Delete();

            return RedirectToAction(nameof(Index));
        }
    }
}
