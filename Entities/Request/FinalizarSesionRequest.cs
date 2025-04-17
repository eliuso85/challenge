using System.ComponentModel.DataAnnotations;

namespace Domain.Request;

public class FinalizarSesionRequest
{
    [Required(ErrorMessage = "El sessionId es requerida")]
    public Guid SessionId { get; set; }

    [Required(ErrorMessage = "El Id del recurso es requerida.")]
    public int RecursoId { get; set; }
}
