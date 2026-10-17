using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace stoiko3.Controllers
{
    public class EjercicioMusculoController : Controller
    {
        // GET: EjercicioMusculoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EjercicioMusculoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EjercicioMusculoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EjercicioMusculoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EjercicioMusculoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EjercicioMusculoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EjercicioMusculoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EjercicioMusculoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
