using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Domain.DTO;

public class User
{
    // Clave primaria de la entidad
    public int Id { get; set; }

    // Dirección de correo electrónico del usuario, se valida que tenga el formato adecuado.
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    // Contraseña sin encriptar
    [MaxLength(10)]
    [MinLength(3)]
    public string  Password { get; set; } = string.Empty;

    //campos adicionales pueden incluirse, como nombre, rol, etc.
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    // Ejemplo de campo para el rol del usuario (p.ej., "User", "Admin")
    [Required]
    [MaxLength(50)]
    public string Role { get; set; } = "User";
}
