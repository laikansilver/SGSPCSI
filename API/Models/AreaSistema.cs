namespace SGSPCSI.API.Models
{
    public class AreaSistema
    {
        public int AreaSistemaId { get; set; }
        public int AreaId { get; set; }
        public int SistemaId { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaAlta { get; set; }

        // Navigation properties
        public virtual Area Area { get; set; } = null!;
        public virtual Sistema Sistema { get; set; } = null!;
    }
}
