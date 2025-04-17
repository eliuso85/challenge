using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class SesionActiva
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public DateTime FechaExpiracion { get; set; }
    }
}
