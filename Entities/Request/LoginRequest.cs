using System.ComponentModel.DataAnnotations;

namespace Domain.Request;
public class LoginRequest
{
    [Required(ErrorMessage = "El email es obligatorio.")]
    [EmailAddress(ErrorMessage = "El email no tiene un formato válido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = $"La contraseña es obligatoria.")]
    [MinLength(3, ErrorMessage = $"La contraseña debe tener al menos 3 caracteres.")]
    [MaxLength(10,ErrorMessage = $"La contraseña debe tener maximo 10 caracteres.")]
    public string Password { get; set; } = string.Empty;
}
