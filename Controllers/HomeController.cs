using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Supabase;
using stoiko3.Models;

namespace stoiko3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Client _supabaseClient;

        public HomeController(ILogger<HomeController> logger, Client supabaseClient)
        {
            _logger = logger;
            _supabaseClient = supabaseClient;
        }

        // 1. GET: /Home/Bienvenida (Página principal / Landing Page por defecto)
        [HttpGet]
        public IActionResult Bienvenida()
        {
            return View();
        }

        // 2. GET: /Home/Index (Perfil de Usuario / Pantalla con Login)
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // 3. POST: /Home/Index (Procesa el formulario de Login enviado desde Index.cshtml)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Por favor, ingresa tu correo y contraseña.";
                return View();
            }

            try
            {
                // Buscar usuario en Supabase por Email
                var response = await _supabaseClient
                    .From<Usuario>()
                    .Where(x => x.Email == email)
                    .Get();

                var usuario = response.Models.FirstOrDefault();

                // Verificar que exista el usuario y que la contraseña coincida
                // Cambiamos 'Password' por 'PassUsu'
                if (usuario != null && usuario.PassUsu == password)
                {
                    HttpContext.Session.SetString("UsuarioEmail", usuario.Email);
                    return RedirectToAction("Index");
                }

                ViewBag.ErrorMessage = "Credenciales incorrectas. Verifica tus datos.";
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar iniciar sesión");
                ViewBag.ErrorMessage = $"Error al conectar con la base de datos: {ex.Message}";
                return View();
            }
        }

        // GET: /Home/Privacy
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        // Acción para probar la conexión con Supabase
        [HttpGet]
        public async Task<IActionResult> TestSupabase()
        {
            try
            {
                var response = await _supabaseClient
                    .From<Usuario>()
                    .Limit(1)
                    .Get();

                return Content($"✅ ¡CONEXIÓN EXITOSA A SUPABASE! Registros de prueba: {response.Models.Count}");
            }
            catch (Exception ex)
            {
                return Content($"❌ ERROR DE CONEXIÓN A SUPABASE:\n\nMensaje: {ex.Message}\n\nDetalle interno: {ex.InnerException?.Message}");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}