namespace stoiko3.Models
{
    public class RegistroEntrenamientoViewModel
    {
        public List<EjercicioInputViewModel> Ejercicios { get; set; } = new();
    }

    public class EjercicioInputViewModel
    {
        public int IdEjercicio { get; set; }
        public string NomEjercicio { get; set; } = string.Empty;
        public bool Seleccionado { get; set; }
        public int Repeticiones { get; set; }
    }
}
