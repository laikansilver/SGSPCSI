namespace SGSPCSI.API.DTOs
{
    public class ProyectoDto
    {
        public long ProyectoId { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string EstadoProyecto { get; set; } = null!;
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinPlaneada { get; set; }
        public DateTime? FechaFinReal { get; set; }
        public int PmUsuarioId { get; set; }
    }

    public class CreateProyectoDto
    {
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinPlaneada { get; set; }
        public int PmUsuarioId { get; set; }
    }

    public class UpdateProyectoDto
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string EstadoProyecto { get; set; } = null!;
        public DateTime? FechaFinPlaneada { get; set; }
        public DateTime? FechaFinReal { get; set; }
    }
}
