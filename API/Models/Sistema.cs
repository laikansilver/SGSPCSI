namespace SGSPCSI.API.Models
{
    public class Sistema
    {
        public int SistemaId { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Navigation properties
        public virtual ICollection<AreaSistema> AreaSistemas { get; set; } = new List<AreaSistema>();
        public virtual ICollection<SistemaDesarrollador> SistemasDesarrollador { get; set; } = new List<SistemaDesarrollador>();
        public virtual ICollection<Solicitud> Solicitudes { get; set; } = new List<Solicitud>();
    }
}
