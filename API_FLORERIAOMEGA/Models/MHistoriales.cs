namespace API_FLORERIAOMEGA.Models
{
    public class MHistoriales
        
    {
        public int idHistorial { get; set; }
        public DateTime? FechaHora { get; set; }
        public string? idUsuario { get; set; }

        public string? Accion { get; set; }
        public string? Clase { get; set; }
        public string? Antes { get; set; }
        public string? Despues { get; set; }
    }
}
