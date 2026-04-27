namespace SGSPCSI.API.Models
{
    public class MenuOpcion
    {
        public int MenuOpcionId { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Ruta { get; set; } = null!;
        public string? Icono { get; set; }
        public int Orden { get; set; }
        public bool Activa { get; set; }
        public int? MenuPadreId { get; set; }

        // Navigation properties
        public virtual MenuOpcion? MenuPadre { get; set; }
        public virtual ICollection<MenuOpcion> MenusHijos { get; set; } = new List<MenuOpcion>();
        public virtual ICollection<RolMenuOpcion> RolMenuOpciones { get; set; } = new List<RolMenuOpcion>();
    }
}
