using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stoiko3.Models;
using Supabase;

namespace stoiko3.Controllers
{
    public class EntrenamientoController : Controller
    {
        private readonly Client _supabase;

        public EntrenamientoController(Client supabase)
        {
            _supabase = supabase;
        }

        //Lista de Ejercicios
        public async Task<IActionResult> Registrar()
        {
            int idUsuario = HttpContext.Session.GetInt32("IdUsuario") ?? 0;
            if (idUsuario == 0) return RedirectToAction("Login", "Usuario");

            var catalogo = await _supabase.From<Ejercicio>().Get();

            var vm = new RegistroEntrenamientoViewModel
            {
                Ejercicios = catalogo.Models.Select(e => new EjercicioInputViewModel
                {
                    IdEjercicio = e.IdEjercicio,
                    NomEjercicio = e.NomEjercicio
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(RegistroEntrenamientoViewModel vm)
        {
            int idUsuario = HttpContext.Session.GetInt32("IdUsuario") ?? 0;
            if (idUsuario == 0) return RedirectToAction("Login", "Usuario");

            var catalogo = (await _supabase.From<Ejercicio>().Get()).Models;
            double totalPuntosSesion = 0;

            foreach (var item in vm.Ejercicios.Where(e => e.Seleccionado && e.Repeticiones > 0))
            {
                var datosEjercicio = catalogo.FirstOrDefault(e => e.IdEjercicio == item.IdEjercicio);
                if (datosEjercicio == null) continue;

                double puntosTotal = item.Repeticiones * datosEjercicio.PuntosEjercicio;
                totalPuntosSesion += puntosTotal;

                // Historial
                await _supabase.From<RegistrarEjercicio>().Insert(new RegistrarEjercicio
                {
                    IdPerfil = idUsuario,
                    IdEjercicio = item.IdEjercicio,
                    Repeticiones = item.Repeticiones,
                    Fecha = DateTime.Now,
                    PuntosObtenidos = puntosTotal
                });

                // Reparto por músculo
                var relaciones = await _supabase.From<EjercicioMusculo>()
                    .Where(em => em.IdEjercicio == item.IdEjercicio)
                    .Get();

                foreach (var relacion in relaciones.Models)
                {
                    // Principal = 100%, secundario = 50% — AJUSTA ESTO si decidiste otro porcentaje
                    double factor = relacion.TipPartici == "principal" ? 1.0 : 0.5;
                    double puntosMusculo = puntosTotal * factor;

                    var progresoExistente = await _supabase.From<ProgresoMuscular>()
                        .Where(p => p.IdPerfil == idUsuario && p.IdMuscular == relacion.IdMusculo)
                        .Get();

                    var progreso = progresoExistente.Models.FirstOrDefault();

                    if (progreso == null)
                    {
                        int idDivision = await BuscarIdDivisionPorPuntaje(puntosMusculo);

                        await _supabase.From<ProgresoMuscular>().Insert(new ProgresoMuscular
                        {
                            IdPerfil = idUsuario,
                            IdMuscular = relacion.IdMusculo,
                            ScorePm = puntosMusculo,
                            IdDivision = idDivision
                        });
                    }
                    else
                    {
                        progreso.ScorePm += puntosMusculo;
                        progreso.IdDivision = await BuscarIdDivisionPorPuntaje(progreso.ScorePm);

                        await _supabase.From<ProgresoMuscular>()
                            .Where(p => p.IdPm == progreso.IdPm)
                            .Update(progreso);
                    }
                }
            }

            // Actualiza el perfil: score general + rango general
            var perfilResponse = await _supabase.From<PerfilUsu>()
                .Where(p => p.IdPerfil == idUsuario)
                .Get();

            var perfil = perfilResponse.Models.FirstOrDefault();
            if (perfil != null)
            {
                perfil.ScoreTotal += totalPuntosSesion;
                perfil.IdDivision = await BuscarIdDivisionPorPuntaje(perfil.ScoreTotal);

                await _supabase.From<PerfilUsu>()
                    .Where(p => p.IdPerfil == idUsuario)
                    .Update(perfil);
            }

            return RedirectToAction("MiPerfil", "PerfilUsu");
        }

        private async Task<int> BuscarIdDivisionPorPuntaje(double puntos)
        {
            var catalogo = await _supabase.From<DivisionGeneral>().Get();

            var rango = catalogo.Models.FirstOrDefault(d =>
                puntos >= d.ScoreMin &&
                (d.ScoreMax == null || puntos <= d.ScoreMax));

            return rango?.IdDivision ?? catalogo.Models.First().IdDivision;
        }

    }
    
}
