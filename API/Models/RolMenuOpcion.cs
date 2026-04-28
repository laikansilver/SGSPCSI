namespace SGSPCSI.API.Models
{
    public class RolMenuOpcion
    {
        public int RolMenuOpcionId { get; set; }
        public int RolId { get; set; }
        public int MenuOpcionId { get; set; }
        public bool PuedeVer { get; set; }
        public bool PuedeEditar { get; set; }
        public bool PuedeAprobar { get; set; }

        // Navigation properties
        public virtual Rol Rol { get; set; } = null!;
        public virtual MenuOpcion MenuOpcion { get; set; } = null!;
    }
}
