namespace API_FLORERIAOMEGA.Models
{
    public class MCierresCajas
    {
        public int idCierreCaja { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public int idUsuario { get; set; }
        public decimal TotalSistema   { get; set; }
        public decimal TotalFisico { get; set; }
        public int idCaja { get; set; }
    }
}
