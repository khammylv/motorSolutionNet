using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty; // "Role" en lugar de "Rol"
        public string UserPassword { get; set; } = string.Empty;
        public string UserIdentification { get; set; } = string.Empty;
        public int CompanyCode { get; set; }
    }
}