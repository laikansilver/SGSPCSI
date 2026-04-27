namespace SGSPCSI.API.Models
{
    public class TipoModificacion
    {
        public int TipoModificacionId { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool Activa { get; set; }

        // Navigation properties
        public virtual ICollection<SolicitudModificacion> SolicitudesModificacion { get; set; } = new List<SolicitudModificacion>();
    }
}
