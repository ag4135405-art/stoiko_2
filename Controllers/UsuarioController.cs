using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using stoiko3.Models;

namespace stoiko3.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly Supabase.Client _supabase;

        public UsuarioController(Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usu)
        {
            if (!ModelState.IsValid) return View(usu);

            // Mapeamos a un nuevo objeto usando las propiedades de columna explícitas
            var usuarioAInsertar = new Usuario
            {
                Email = usu.EmailUsu,
                PassUsu = BCrypt.Net.BCrypt.HashPassword(usu.PasswordUsu),
                FechaRegistro = DateTime.UtcNow,
                FechaInicioSecion = DateTime.UtcNow
            };

            var response = await _supabase.From<Usuario>().Insert(usuarioAInsertar);
            var nuevoUsuario = response.Models.FirstOrDefault();

            if (nuevoUsuario == null) return View(usu);

            HttpContext.Session.SetInt32("IdUsuario", nuevoUsuario.IdUsu);

            return RedirectToAction("Index", "Home");
        }

        // GET: UsuarioController/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: UsuarioController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string correo, string contrasena)
        {
            // Usamos u.Email (que tiene el atributo [Column("email_usu")])
            var response = await _supabase.From<Usuario>()
                .Where(u => u.Email == correo)
                .Get();

            var usuario = response.Models.FirstOrDefault();

            if (usuario == null || string.IsNullOrEmpty(usuario.PassUsu) || !BCrypt.Net.BCrypt.Verify(contrasena, usuario.PassUsu))
            {
                ModelState.AddModelError("", "Correo o contraseña incorrectos");
                return View();
            }

            HttpContext.Session.SetInt32("IdUsuario", usuario.IdUsu);
            return RedirectToAction("Index", "Home");
        }

        // GET: UsuarioController/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult LoginGoogle()
        {
            var props = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(props, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded) return RedirectToAction("Login");

            var email = result.Principal.FindFirstValue(ClaimTypes.Email);

            // Busca por u.Email para coincidir con [Column("email_usu")]
            var response = await _supabase.From<Usuario>()
                .Where(u => u.Email == email)
                .Get();

            var usuario = response.Models.FirstOrDefault();

            if (usuario == null)
            {
                var nuevo = new Usuario
                {
                    Email = email!,
                    PassUsu = BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString()),
                    FechaRegistro = DateTime.UtcNow,
                    FechaInicioSecion = DateTime.UtcNow
                };

                var insertResponse = await _supabase.From<Usuario>().Insert(nuevo);
                usuario = insertResponse.Models.FirstOrDefault();

                HttpContext.Session.SetInt32("IdUsuario", usuario!.IdUsu);
                return RedirectToAction("MiPerfil", "PerfilUsu");
            }

            HttpContext.Session.SetInt32("IdUsuario", usuario.IdUsu);
            return RedirectToAction("Index", "Home");
        }
    }
}