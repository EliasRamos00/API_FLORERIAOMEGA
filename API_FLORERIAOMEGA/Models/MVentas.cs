namespace API_FLORERIAOMEGA.Models
{
    public class MVentas
    {

        public int idVenta { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaHora { get; set; }
        public int Vendedor { get; set; }
        public int tieneFactura { get; set; }
        public int idSucursal { get; set; }


    }
}
