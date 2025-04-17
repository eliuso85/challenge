using System.ComponentModel.DataAnnotations;

namespace Domain.Request;

public class IniciarSesionRequest
{
    [Required(ErrorMessage = "La cantidad es requerida.")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad requerida debe ser mayor a 0.")]
    public int CantidadRequerida { get; set; }

    [Required(ErrorMessage = "El Id del recurso es requerida.")]
    public int RecursoId { get; set; }
}
