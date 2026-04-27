namespace SGSPCSI.API.Models
{
    public class Area
    {
        public int AreaId { get; set; }
        public int? AreaPadreId { get; set; }
        public string? Clave { get; set; }
        public string Nombre { get; set; } = null!;
        public string TipoArea { get; set; } = null!;
        public byte Nivel { get; set; }
        public bool Activa { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Navigation properties
        public virtual Area? AreaPadre { get; set; }
        public virtual ICollection<Area> AreasHijas { get; set; } = new List<Area>();
        public virtual ICollection<UsuarioArea> UsuariosAreas { get; set; } = new List<UsuarioArea>();
        public virtual ICollection<AreaSistema> AreaSistemas { get; set; } = new List<AreaSistema>();
        public virtual ICollection<Solicitud> Solicitudes { get; set; } = new List<Solicitud>();
    }
}
