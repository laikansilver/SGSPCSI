namespace SGSPCSI.API.Models
{
    public class UsuarioArea
    {
        public int UsuarioAreaId { get; set; }
        public int UsuarioId { get; set; }
        public int AreaId { get; set; }
        public bool EsTitular { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        // Navigation properties
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Area Area { get; set; } = null!;
    }
}
