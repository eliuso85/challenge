using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tools;

public class PasswordHelper
{
    //private readonly int _workFactor;

    // Cifra una contraseña en texto plano
    public static string HashPassword(string plainPassword)
    {

        int workFactor = 12; // Entre 10 y 14 es ideal.
        // El segundo parámetro es el work factor (coste). Por defecto es 10.
        return BCrypt.Net.BCrypt.HashPassword(plainPassword, workFactor);
    }

    // Verifica si una contraseña coincide con el hash
    public static bool VerifyPassword(string plainPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
    }

    public class PasswordSettings
    {
        public int BcryptWorkFactor { get; set; }
    }
}
