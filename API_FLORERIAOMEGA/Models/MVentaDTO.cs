namespace API_FLORERIAOMEGA.Models
{
    public class MVentaDTO
    {
        public MVentas Venta { get; set; }
        public List<MVentaDetalle> Detalle { get; set; }
    }
}
