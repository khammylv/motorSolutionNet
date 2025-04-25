using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace MotorSolutionNet.Services
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string name, string role);
        ClaimsPrincipal ValidateToken(string token);
    }
}