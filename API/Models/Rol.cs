namespace SGSPCSI.API.Models
{
    public class Rol
    {
        public int RolId { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Navigation properties
        public virtual ICollection<UsuarioRol> UsuariosRoles { get; set; } = new List<UsuarioRol>();
        public virtual ICollection<RolMenuOpcion> RolMenuOpciones { get; set; } = new List<RolMenuOpcion>();
    }
}
