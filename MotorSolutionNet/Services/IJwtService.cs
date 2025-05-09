using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace MotorSolutionNet.Services
{
    public interface IJwtService
    {
        string GenerateTokenUser(int userId, string name, string role, int companyCode);
        string GenerateTokenCompany(int companyCode, string name);
        ClaimsPrincipal ValidateToken(string token);
    }
}