namespace SGSPCSI.API.Models
{
    public class ActividadReciente
    {
        public long ActividadRecienteId { get; set; }
        public long? SolicitudId { get; set; }
        public long? TareaDesarrolloId { get; set; }
        public int UsuarioId { get; set; }
        public string TipoActividad { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public string? Detalle { get; set; }
        public DateTime FechaActividad { get; set; }
        public bool VisibleParaPm { get; set; }
        public bool VisibleParaDesarrollador { get; set; }

        // Navigation properties
        public virtual Solicitud? Solicitud { get; set; }
        public virtual TareaDesarrollo? TareaDesarrollo { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
