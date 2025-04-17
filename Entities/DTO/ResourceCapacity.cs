namespace Domain.DTO;

public class ResourceCapacity
{
    public int Id { get; set; }             // Identificador del recurso (por ejemplo, 1)
    public int TotalCapacity { get; set; }    // Capacidad total disponible
    public int UsedCapacity { get; set; }     // Capacidad actualmente en uso
}
