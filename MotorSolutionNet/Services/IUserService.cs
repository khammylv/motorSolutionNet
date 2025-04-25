using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.Services
{
    public interface IUserService
    {

        string HashPassword(string password);
        bool VerifyPassword(string inputPassword, string storedHash);
    }
}