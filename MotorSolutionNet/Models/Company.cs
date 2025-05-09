using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.Models
{
    public class Company
    {
        public int CompanyCode { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Nit { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;
        public string CompanyPhone { get; set; } = string.Empty;
        public string CompanyEmail { get; set; } = string.Empty;
        public string LegalRepresentative { get; set; } = string.Empty;
        
        public string CompanyPassword { get; set; } = string.Empty;
    }
}