using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.Services
{
    public class AuthService : IAuthService
    {
        public string HashPassword(string password)
        {
            // Genera el hash bcrypt de la contraseña con un salt generado aleatoriamente
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }
        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            // Verifica si la contraseña ingresada coincide con el hash almacenado
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
            return isPasswordValid;
        }
    }
}