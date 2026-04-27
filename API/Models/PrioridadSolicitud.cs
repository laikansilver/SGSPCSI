namespace SGSPCSI.API.Models
{
    public class PrioridadSolicitud
    {
        public int PrioridadSolicitudId { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int Orden { get; set; }
        public bool Activa { get; set; }

        // Navigation properties
        public virtual ICollection<Solicitud> Solicitudes { get; set; } = new List<Solicitud>();
        public virtual ICollection<TareaDesarrollo> Tareas { get; set; } = new List<TareaDesarrollo>();
    }
}
