namespace SGSPCSI.API.DTOs
{
    public class TareaDesarrolloDto
    {
        public long TareaDesarrolloId { get; set; }
        public long SolicitudId { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int PrioridadSolicitudId { get; set; }
        public string EstadoTarea { get; set; } = null!;
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public DateTime? FechaCierre { get; set; }
    }

    public class CreateTareaDesarrolloDto
    {
        public long SolicitudId { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int PrioridadSolicitudId { get; set; }
    }

    public class UpdateTareaDesarrolloDto
    {
        public string Titulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int PrioridadSolicitudId { get; set; }
        public string EstadoTarea { get; set; } = null!;
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public DateTime? FechaCierre { get; set; }
    }
}
