namespace SGSPCSI.API.DTOs
{
    public class SolicitudDto
    {
        public long SolicitudId { get; set; }
        public string Folio { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int AreaSolicitanteId { get; set; }
        public int? SistemaId { get; set; }
        public int TipoSolicitudId { get; set; }
        public int PrioridadSolicitudId { get; set; }
        public int EstadoSolicitudId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public DateTime? FechaResolucion { get; set; }
        public decimal? EsfuerzoHoras { get; set; }
    }

    public class CreateSolicitudDto
    {
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int AreaSolicitanteId { get; set; }
        public int? SistemaId { get; set; }
        public int TipoSolicitudId { get; set; }
        public int PrioridadSolicitudId { get; set; }
    }

    public class UpdateSolicitudDto
    {
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int TipoSolicitudId { get; set; }
        public int PrioridadSolicitudId { get; set; }
        public int EstadoSolicitudId { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public decimal? EsfuerzoHoras { get; set; }
    }
}
