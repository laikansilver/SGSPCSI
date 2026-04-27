namespace SGSPCSI.API.Models
{
    public class SolicitudRequerimientoTecnico
    {
        public long SolicitudRequerimientoTecnicoId { get; set; }
        public long SolicitudId { get; set; }
        public string? ArquitecturaPropuesta { get; set; }
        public string? AlcanceTecnico { get; set; }
        public string? Dependencias { get; set; }
        public string? CriteriosAceptacion { get; set; }
        public string? Riesgos { get; set; }
        public string? PlanPruebas { get; set; }
        public string? Observaciones { get; set; }
        public int ElaboradoPorUsuarioId { get; set; }
        public DateTime FechaElaboracion { get; set; }

        // Navigation properties
        public virtual Solicitud Solicitud { get; set; } = null!;
        public virtual Usuario ElaboradoPor { get; set; } = null!;
    }
}
