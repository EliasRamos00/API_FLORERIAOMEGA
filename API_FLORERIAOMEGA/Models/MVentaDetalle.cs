namespace API_FLORERIAOMEGA.Models
{
    public class MVentaDetalle
    {
        public int idArticulo { get; set; }
        public int idVenta { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
    }
}
