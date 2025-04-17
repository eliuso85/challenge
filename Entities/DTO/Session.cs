namespace Domain.DTO;

public class Session
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public int RecursoId { get; set; }
    public DateTime StartTime { get; set; }
    public int? CantidadRequerida { get; set; } = 0; // La cantidad de capacidad que usa la sesión
    public string Estado { get; set; } = string.Empty; // Por ejemplo, "activa" o "finalizada"
    public Guid SessionId { get; set; }
}
