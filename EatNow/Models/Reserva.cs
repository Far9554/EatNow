using System.ComponentModel.DataAnnotations;

namespace EatNow.Models
{
    public class Reserva
    {
        [Required]
        public int IdReserva { get; set; }

        [Required]
        public DateTime Inicio { get; set; }

        [Required]
        public DateTime Fin {  get; set; }

        [Required]
        public int RIdCliente { get; set; }

        public int RIdEstadoReserva { get; set; }

        [Required]
        public int RIdCasilla { get; set; }
    }
}
