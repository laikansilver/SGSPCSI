namespace SGSPCSI.API.Models
{
    public class SistemaDesarrollador
    {
        public int SistemaDesarrolladorId { get; set; }
        public int SistemaId { get; set; }
        public int UsuarioId { get; set; }
        public string TipoParticipacion { get; set; } = null!;
        public bool Activo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        // Navigation properties
        public virtual Sistema Sistema { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
