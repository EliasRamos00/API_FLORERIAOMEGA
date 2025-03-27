namespace API_FLORERIAOMEGA.Models
{
    public class MArticulosDTO
    {
        public int idArticulo { get; set; }
        public string? Foto { get; set; }
        public string? Color { get; set; }
        public string? Descripcion { get; set; }
        public string? Tamanio { get; set; }
        public string? CodigoBarras { get; set; }
        public int IdCategoria { get; set; }

        // INVENTARIO
        public int idInventario { get; set; }
        public int? Stock { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }

        public decimal? PrecioVenta { get; set; }
        public decimal? PrecioCompra { get; set; }



    }
}
