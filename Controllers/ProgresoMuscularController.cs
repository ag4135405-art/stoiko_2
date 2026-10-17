using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stoiko3.Models;

namespace stoiko3.Controllers
{
    public class ProgresoMuscularController : Controller
    {
        private readonly Supabase.Client _supabase;

        public ProgresoMuscularController(Supabase.Client supabase)
        {
            _supabase = supabase;
        }


        // GET: ProgresoMuscularController
        public async Task<IActionResult> Index()
        {
            var responde = await _supabase.From<ProgresoMuscular>().Get();
            var progrestMusculars = responde.Models;
            return View(progrestMusculars);
        }

        // GET: ProgresoMuscularController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _supabase.From<ProgresoMuscular>()
                .Where(d => d.IdPm == id)
                .Get();

            var dp = response.Models.FirstOrDefault();
            if (dp == null) return NotFound();

            return View(dp);
        }

        // GET: ProgresoMuscularController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProgresoMuscularController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProgresoMuscular dp)
        {
            if (!ModelState.IsValid) return View(dp);

            await _supabase.From<ProgresoMuscular>().Insert(dp);
            return RedirectToAction(nameof(Index));
        }

        // GET: ProgresoMuscularController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _supabase.From<ProgresoMuscular>()
                .Where(d => d.IdPm == id)
                .Get();

            var dp = response.Models.FirstOrDefault();
            if (dp == null) return NotFound();

            return View(dp);
        }

        // POST: ProgresoMuscularController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProgresoMuscular dp)
        {
            if (id != dp.IdPm) return BadRequest();
            if (!ModelState.IsValid) return View(dp);

            await _supabase.From<ProgresoMuscular>()
                .Where(d => d.IdPm == id)
                .Update(dp);

            return RedirectToAction(nameof(Index));
        }

        // GET: ProgresoMuscularController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _supabase.From<ProgresoMuscular>()
                .Where(d => d.IdPm == id)
                .Get();

            var dp = response.Models.FirstOrDefault();
            if (dp == null) return NotFound();

            return View(dp);
        }

        // POST: ProgresoMuscularController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ProgresoMuscular dp)
        {
            await _supabase.From<ProgresoMuscular>()
                .Where(d => d.IdPm == id)
                .Delete();

            return RedirectToAction(nameof(Index));
        }
    }
}
