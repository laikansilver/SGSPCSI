namespace SGSPCSI.API.Models
{
    public class TipoSolicitud
    {
        public int TipoSolicitudId { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool Activa { get; set; }

        // Navigation properties
        public virtual ICollection<Solicitud> Solicitudes { get; set; } = new List<Solicitud>();
    }
}
