namespace SGSPCSI.API.DTOs
{
    public class AreaDto
    {
        public int AreaId { get; set; }
        public int? AreaPadreId { get; set; }
        public string? Clave { get; set; }
        public string Nombre { get; set; } = null!;
        public string TipoArea { get; set; } = null!;
        public byte Nivel { get; set; }
        public bool Activa { get; set; }
    }

    public class CreateAreaDto
    {
        public int? AreaPadreId { get; set; }
        public string? Clave { get; set; }
        public string Nombre { get; set; } = null!;
        public string TipoArea { get; set; } = null!;
        public byte Nivel { get; set; }
    }

    public class UpdateAreaDto
    {
        public string? Clave { get; set; }
        public string Nombre { get; set; } = null!;
        public string TipoArea { get; set; } = null!;
        public byte Nivel { get; set; }
        public bool Activa { get; set; }
    }
}
